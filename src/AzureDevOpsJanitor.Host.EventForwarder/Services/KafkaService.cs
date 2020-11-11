using System;
using System.Threading;
using System.Threading.Tasks;
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

        public KafkaService(
            ILogger<KafkaService> logger,
            IServiceProvider serviceProvider)
        {
            Console.WriteLine("Starting KafkaService");
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        void Emit(string topic, string payload)
        {
            throw new System.NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = Task.Factory.StartNew(async () =>
                {

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

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}