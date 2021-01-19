using Confluent.Kafka;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Abstractions.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Kafka
{
    public class KafkaIntegrationEventHandler : IEventHandler<IIntegrationEvent>
    {
        private readonly IOptions<KafkaOptions> _options;
        private readonly IProducer<Ignore, IIntegrationEvent> _producer;

        public KafkaIntegrationEventHandler(IProducer<Ignore, IIntegrationEvent> producer, IOptions<KafkaOptions> options)
        {
            _producer = producer ?? throw new ArgumentException(null, nameof(producer)); ;
            _options = options ?? throw new ArgumentException(null, nameof(options)); ;
        }

        public async Task Handle(IIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var message = new Message<Ignore, IIntegrationEvent>()
            {
                Value = notification
            };

            foreach (var topic in _options.Value.Topics)
            {
                await _producer.ProduceAsync(topic, message, cancellationToken);
            }
        }
    }
}
