using Confluent.Kafka;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Abstractions.Events;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Kafka
{
    public class KafkaIntegrationEventHandler : IEventHandler<IIntegrationEvent>
    {
        private readonly IOptions<KafkaOptions> _options;
        private readonly IProducer<Ignore, IIntegrationEvent> _producer;

        public KafkaIntegrationEventHandler(IProducer<Ignore, IIntegrationEvent> producer = default, IOptions<KafkaOptions> options = default) 
        {
            _producer = producer;
            _options = options;
        }

        public async Task Handle(IIntegrationEvent notification, CancellationToken cancellationToken)
        {
            if (_options?.Value.Topics == null || _options?.Value.Topics.Count() == 0)
            {
                return;
            }

            var message = new Message<Ignore, IIntegrationEvent>()
            {
                Value = notification
            };

            foreach (var topic in _options?.Value.Topics)
            {
                await _producer?.ProduceAsync(topic, message, cancellationToken);
            }
        }
    }
}
