using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using AzureDevOpsJanitor.Host.EventForwarder.Enablers.Kafka;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AzureDevOpsJanitor.Host.EventForwarder.Services
{
    public class KafkaService : IHostedService
    {
        private ILogger<KafkaService> _logger;
        private Task _executingTask;
        private readonly IServiceProvider _serviceProvider;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly ConcurrentQueue<string> _queue;
        private readonly KafkaProducerFactory _producerFactory;

        public KafkaService(
            ILogger<KafkaService> logger,
            IServiceProvider serviceProvider,
            KafkaProducerFactory kafkaProducerFactory)
        {
            Console.WriteLine("Starting KafkaService");
            _logger = logger;
            _serviceProvider = serviceProvider;
            _queue = new ConcurrentQueue<string>();
            _producerFactory = kafkaProducerFactory;
        }

        void Emit(string topic, string payload)
        {
            throw new System.NotImplementedException();
        }

        public void Queue(string val)
        {
            _queue.Enqueue(val);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = Task.Factory.StartNew(async () =>
                {
                    while (!_cancellationTokenSource.IsCancellationRequested)
                    {
                        string val;
                        var producer = _producerFactory.Create();
                        while (_queue.TryDequeue(out val))
                        {
                            Console.WriteLine(val);
                            await producer.ProduceAsync(topic: "REPLACE-ME", message: new Message<string, string>()
                            {
                                Key = Guid.NewGuid().ToString(),
                                Value = "PAYLOAD-HERE"
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