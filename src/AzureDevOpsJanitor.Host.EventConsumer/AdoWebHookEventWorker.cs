using AutoMapper;
using AzureDevOpsJanitor.Host.EventConsumer.Strategies;
using CloudEngineering.CodeOps.Infrastructure.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CloudEngineering.CodeOps.Abstractions.Facade;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.EventConsumer
{
    public class AdoWebHookEventWorker : KafkaConsumerService
    {
        public AdoWebHookEventWorker(ILogger<KafkaConsumerService> logger, IOptions<KafkaOptions> options, IMapper mapper, IFacade applicationFacade) : base(logger, options, new AdoWebHookConsumptionStrategy(mapper, applicationFacade))
        {
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting: {0}", nameof(AdoWebHookEventWorker));

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping: {0}", nameof(AdoWebHookEventWorker));

            return base.StopAsync(cancellationToken);
        }
    }
}
