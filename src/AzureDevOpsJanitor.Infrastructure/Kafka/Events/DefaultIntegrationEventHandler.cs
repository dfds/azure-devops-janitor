﻿using Confluent.Kafka;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Abstractions.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Kafka.Events
{
    public class DefaultIntegrationEventHandler : IEventHandler<IIntegrationEvent>
    {
        private readonly IOptions<KafkaOptions> _options;
        private readonly IProducer<string, IIntegrationEvent> _producer;

        public DefaultIntegrationEventHandler(IProducer<string, IIntegrationEvent> producer, IOptions<KafkaOptions> options)
        {
            _producer = producer ?? throw new ArgumentException(null, nameof(producer));
            _options = options ?? throw new ArgumentException(null, nameof(options));
        }

        public async Task Handle(IIntegrationEvent notification, CancellationToken cancellationToken = default)
        {
            var message = new Message<string, IIntegrationEvent>()
            {
                Value = notification
            };

            foreach (var topic in notification.Topics ?? _options.Value.Topics)
            {
                await _producer.ProduceAsync(topic, message, cancellationToken);
            }
        }
    }
}
