using AutoMapper;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
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
    public sealed class VstsWebHookConsumptionStrategy : IStrategy<ConsumeResult<Ignore, string>>
    {
        private readonly IFacade _applicationFacade;
        private readonly IMapper _mapper;

        public VstsWebHookConsumptionStrategy(IMapper mapper, IFacade applicationFacade)
        {
            _mapper = mapper ?? throw new ArgumentException(null, nameof(mapper));
            _applicationFacade = applicationFacade ?? throw new ArgumentException(null, nameof(applicationFacade));
        }

        public ValueTask<ConsumeResult<Ignore, string>> Apply(ConsumeResult<Ignore, string> target, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(target.Message.Value))
            {
                var payload = JsonSerializer.Deserialize<IIntegrationEvent>(target.Message.Value);
                IAggregateRoot aggregate;

                //TODO: Finish switch 
                switch (payload.Type)
                {
                    default:
                        var dto = JsonSerializer.Deserialize<BuildDto>(payload.Payload?.GetString());

                        aggregate = _mapper.Map<IAggregateRoot>(dto);
                        break;
                }

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
