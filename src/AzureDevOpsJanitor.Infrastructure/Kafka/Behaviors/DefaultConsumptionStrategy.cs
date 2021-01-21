using AutoMapper;
using Confluent.Kafka;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Behaviours;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Facade;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Kafka.Behaviors
{
    public sealed class DefaultConsumptionStrategy : IStrategy<ConsumeResult<Ignore, string>>
    {
        private readonly IFacade _applicationFacade;
        private readonly IMapper _mapper;

        public DefaultConsumptionStrategy(IMapper mapper, IFacade applicationFacade)
        {
            _mapper = mapper ?? throw new ArgumentException(null, nameof(mapper));
            _applicationFacade = applicationFacade ?? throw new ArgumentException(null, nameof(applicationFacade));
        }

        public ValueTask<ConsumeResult<Ignore, string>> Apply(ConsumeResult<Ignore, string> target, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(target.Message.Value))
            {
                var envelope = JsonSerializer.Deserialize<IIntegrationEvent>(target.Message.Value);
                var aggregate = JsonSerializer.Deserialize<IAggregateRoot>(envelope.Payload?.GetString());
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
