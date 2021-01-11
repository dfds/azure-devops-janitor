using AutoMapper;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ResourceProvisioning.Abstractions.Facade;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.KafkaWorker
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;
        private readonly IMapper _mapper;
        private readonly IFacade _applicationFacade;

        public Worker(ILogger<Worker> logger, IMapper mapper, IFacade applicationFacade)
		{
            _logger = logger;
            _mapper = mapper;
            _applicationFacade = applicationFacade;
        }

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var config = new ConsumerConfig
			{
				BootstrapServers = "pkc-e8wrm.eu-central-1.aws.confluent.cloud:9092",
				GroupId = "azure-devsop-janitor-worker",
				EnableAutoCommit = false,
				StatisticsIntervalMs = 5000,
				SessionTimeoutMs = 6000,
				AutoOffsetReset = AutoOffsetReset.Earliest,
				EnablePartitionEof = true
			};

            const int commitPeriod = 5;

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

            consumer.Subscribe("pub.segment-ui-beorp.default");

            try
            {
                while (true)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(stoppingToken);

                        if (consumeResult.IsPartitionEOF)
                        {
                            _logger.LogInformation($"Reached end of topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}.");

                            continue;
                        }

                        _logger.LogInformation($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");

                        if (!string.IsNullOrEmpty(consumeResult.Message.Value))
                        {
                            var messagePayload = JsonSerializer.Deserialize<dynamic>(consumeResult.Message.Value);

                            //TODO: Implement maps.
                            var mappedObject = _mapper.Map(messagePayload);
                            
                            //TODO: Perform neccessary updates via application facade.
                        }

                        if (consumeResult.Offset % commitPeriod == 0)
                        {
                            // The Commit method sends a "commit offsets" request to the Kafka
                            // cluster and synchronously waits for the response. This is very
                            // slow compared to the rate at which the consumer is capable of
                            // consuming messages. A high performance application will typically
                            // commit offsets relatively infrequently and be designed handle
                            // duplicate messages in the event of failure.
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
