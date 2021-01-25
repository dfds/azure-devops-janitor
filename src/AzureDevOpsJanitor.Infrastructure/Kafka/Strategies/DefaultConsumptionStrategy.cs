using AutoMapper;
using Confluent.Kafka;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Facade;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Kafka.Strategies
{
    public sealed class DefaultConsumptionStrategy : ConsumtionStrategy
    {
        public DefaultConsumptionStrategy(IMapper mapper, IFacade applicationFacade) : base(mapper, applicationFacade)
        {
        }

        public override ValueTask<ConsumeResult<string, string>> Apply(ConsumeResult<string, string> target, CancellationToken cancellationToken)
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

            return new ValueTask<ConsumeResult<string, string>>(target);
        }
    }
}
