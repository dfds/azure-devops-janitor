using AutoMapper;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Facade;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Kafka
{
    public class KafkaConsumerService : BackgroundService
    {
        protected readonly ILogger<KafkaConsumerService> _logger;
        protected readonly IOptions<KafkaOptions> _options;
        protected readonly IMapper _mapper;
        protected readonly IFacade _applicationFacade;

        public KafkaConsumerService(ILogger<KafkaConsumerService> logger, IMapper mapper, IOptions<KafkaOptions> options, IFacade applicationFacade)
        {
            _logger = logger ?? throw new ArgumentException(null, nameof(logger));
            _options = options ?? throw new ArgumentException(null, nameof(options));
            _mapper = mapper ?? throw new ArgumentException(null, nameof(mapper));
            _applicationFacade = applicationFacade ?? throw new ArgumentException(null, nameof(applicationFacade));
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig(_options.Value.Configuration)
            {
                EnablePartitionEof = _options.Value.EnablePartitionEof,
                StatisticsIntervalMs = _options.Value.StatisticsIntervalMs
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config)
                .SetErrorHandler((_, e) => _logger.LogError($"Error: {e.Reason}", e))
                .SetStatisticsHandler((_, json) => _logger.LogDebug($"Statistics: {json}"))
                .SetPartitionsAssignedHandler((c, partitions) =>
                {
                    _logger.LogInformation($"Assigned partitions: [{string.Join(", ", partitions)}]");
                })
                .SetPartitionsRevokedHandler((c, partitions) =>
                {
                    _logger.LogInformation($"Revoking assignment: [{string.Join(", ", partitions)}]");
                })
                .Build();

            foreach (var topic in _options.Value.Topics)
            {
                consumer.Subscribe(topic);
            }

            try
            {
                while (true)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(cancellationToken);

                        if (consumeResult.IsPartitionEOF)
                        {
                            _logger.LogInformation($"Reached end of topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}.");

                            continue;
                        }

                        _logger.LogInformation($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");

                        if (!string.IsNullOrEmpty(consumeResult.Message.Value))
                        {
                            var integrationEvent = JsonSerializer.Deserialize<IIntegrationEvent>(consumeResult.Message.Value);
                            var aggregate = JsonSerializer.Deserialize<IAggregateRoot>(integrationEvent.Payload.GetString());
                            var command = _mapper.Map<IAggregateRoot, ICommand<IAggregateRoot>>(aggregate);

                            if (command != null)
                            {
                                _applicationFacade.Execute(command, cancellationToken);
                            }
                        }

                        if (consumeResult.Offset % _options.Value.CommitPeriod == 0)
                        {
                            try
                            {
                                consumer.Commit(consumeResult);
                            }
                            catch (KafkaException e)
                            {
                                _logger.LogError($"Commit error: {e.Error.Reason}", e);
                            }
                        }
                    }
                    catch (ConsumeException e)
                    {
                        _logger.LogError($"Consume error: {e.Error.Reason}", e);
                    }
                }
            }
            catch (OperationCanceledException e)
            {
                _logger.LogInformation("Closing consumer.", e);

                consumer.Close();
            }

            return Task.CompletedTask;
        }
    }
}
