using AutoMapper;
using AzureDevOpsJanitor.Infrastructure.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Abstractions.Facade;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.KafkaWorker
{
    public class Worker : KafkaConsumerService
    {
        public Worker(ILogger<KafkaConsumerService> logger, IMapper mapper, IOptions<KafkaOptions> options, IFacade applicationFacade) : base(logger, mapper, options, applicationFacade)
        {
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting: {0}", nameof(Worker));

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping: {0}", nameof(Worker));

            return base.StopAsync(cancellationToken);
        }
    }
}
