﻿using AutoMapper;
using AzureDevOpsJanitor.Host.EventConsumer.Strategies;
using AzureDevOpsJanitor.Infrastructure.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Abstractions.Facade;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.EventConsumer
{
    public class VstsWebHookEventWorker : KafkaConsumerService
    {
        public VstsWebHookEventWorker(ILogger<KafkaConsumerService> logger, IOptions<KafkaOptions> options, IMapper mapper, IFacade applicationFacade) : base(logger, options, new VstsWebHookConsumptionStrategy(mapper, applicationFacade))
        {
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting: {0}", nameof(VstsWebHookEventWorker));

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping: {0}", nameof(VstsWebHookEventWorker));

            return base.StopAsync(cancellationToken);
        }
    }
}