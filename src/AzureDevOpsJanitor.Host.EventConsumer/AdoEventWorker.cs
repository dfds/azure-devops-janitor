using AutoMapper;
using AzureDevOpsJanitor.Application;
using AzureDevOpsJanitor.Host.EventConsumer.Strategies;
using CloudEngineering.CodeOps.Infrastructure.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.EventConsumer
{
    public class AdoEventWorker : KafkaConsumerService
    {
        public AdoEventWorker(ILogger<KafkaConsumerService> logger, IOptions<KafkaOptions> options, IMapper mapper, IApplicationFacade applicationFacade) : base(logger, options, new AdoWebHookConsumptionStrategy(mapper, applicationFacade))
        {
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting: {0}", nameof(AdoEventWorker));

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping: {0}", nameof(AdoEventWorker));

            return base.StopAsync(cancellationToken);
        }
    }
}
