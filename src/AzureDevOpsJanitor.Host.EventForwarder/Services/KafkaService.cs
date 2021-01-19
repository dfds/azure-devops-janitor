using AzureDevOpsJanitor.Host.EventForwarder.Enablers.Kafka;
using AzureDevOpsJanitor.Host.EventForwarder.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.EventForwarder.Services
{
    //TODO: Deprecate this in favor of mediator passed IIntegrationEvent publishing and KafkaEventHandler? This has the added benefit of decoupling our controllers from the service.
    public class KafkaService : IHostedService
    {
        private ILogger<KafkaService> _logger;
        private Task _executingTask;
        private readonly IServiceProvider _serviceProvider;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly ConcurrentQueue<ForwardContent> _queue;
        private readonly KafkaProducerFactory _producerFactory;

        public KafkaService(
            ILogger<KafkaService> logger,
            IServiceProvider serviceProvider,
            KafkaProducerFactory kafkaProducerFactory)
        {
            Console.WriteLine("Starting KafkaService");
            _logger = logger;
            _serviceProvider = serviceProvider;
            _queue = new ConcurrentQueue<ForwardContent>();
            _producerFactory = kafkaProducerFactory;
        }

        void Emit(string topic, string payload)
        {
            throw new System.NotImplementedException();
        }

        public void Queue(ForwardContent val)
        {
            _queue.Enqueue(val);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = Task.Factory.StartNew(async () =>
                {
                    var producer = _producerFactory.Create();
                    while (!_cancellationTokenSource.IsCancellationRequested)
                    {
                        ForwardContent val;
                        while (_queue.TryDequeue(out val))
                        {
                            await producer.ProduceAsync(topic: val.Topic, message: new Message<string, string>()
                            {
                                Key = Guid.NewGuid().ToString(),
                                Value = val.Payload
                            });
                        }

                        Thread.Sleep(500);
                    }
                }, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default)
                .ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        _logger.LogError(task.Exception, "KafkaService crashed");
                    }
                }, cancellationToken);

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                _cancellationTokenSource.Cancel();
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));
            }

            Console.WriteLine("Stopping KafkaService");

            _cancellationTokenSource.Dispose();
        }
    }
}