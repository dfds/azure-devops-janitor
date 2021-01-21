using AutoMapper;
using Confluent.Kafka;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Facade;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Kafka.Behaviors
{
    public sealed class DefaultConsumptionStrategy : AbstractConsumtionStrategy
    {
        public DefaultConsumptionStrategy(IMapper mapper, IFacade applicationFacade) : base(mapper, applicationFacade)
        {
        }

        public override ValueTask<ConsumeResult<Ignore, string>> Apply(ConsumeResult<Ignore, string> target, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(target.Message.Value))
            {
                var @event = JsonSerializer.Deserialize<IIntegrationEvent>(target.Message.Value);
                var aggregate = JsonSerializer.Deserialize<IAggregateRoot>(@event.Payload?.GetString());
                var command = _mapper.Map<IAggregateRoot, ICommand<IAggregateRoot>>(aggregate);

                if (command != null)
                {
                    _applicationFacade.Execute(command, cancellationToken);
                }
            }

            return new ValueTask<ConsumeResult<Ignore, string>>(target);
        }
    }
}
