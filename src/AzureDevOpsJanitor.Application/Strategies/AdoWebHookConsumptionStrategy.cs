using AutoMapper;
using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Commands;
using CloudEngineering.CodeOps.Abstractions.Events;
using CloudEngineering.CodeOps.Infrastructure.Azure.DevOps.DataTransferObjects;
using CloudEngineering.CodeOps.Infrastructure.Azure.DevOps.Events;
using CloudEngineering.CodeOps.Infrastructure.Kafka.Strategies;
using Confluent.Kafka;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Strategies
{
    public sealed class AdoWebHookConsumptionStrategy : ConsumtionStrategy
    {
        public AdoWebHookConsumptionStrategy(IMapper mapper, IApplicationFacade applicationFacade) : base(mapper, applicationFacade)
        {

        }

        public override Task Apply(ConsumeResult<string, string> target, CancellationToken cancellationToken)
        {
            var payload = target.Message.Value;

            if (!string.IsNullOrEmpty(payload))
            {
                var @event = JsonSerializer.Deserialize<IntegrationEvent>(payload);
                var adoEvent = _mapper.Map<AdoEvent>(@event.Payload.Value);
                var adoDto = _mapper.Map<AdoDto>(adoEvent);
                var aggregateRoot = _mapper.Map<IAggregateRoot>(adoDto);
                var command = _mapper.Map<IAggregateRoot, ICommand<IAggregateRoot>>(aggregateRoot);

                if (command != null)
                {
                    _applicationFacade.Execute(command, cancellationToken);
                }
            }

            return Task.CompletedTask;
        }
    }
}
