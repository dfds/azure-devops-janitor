using AutoMapper;
using CloudEngineering.CodeOps.Infrastructure.Kafka.Strategies;
using CloudEngineering.CodeOps.Infrastructure.AzureDevOps.DataTransferObjects;
using CloudEngineering.CodeOps.Infrastructure.AzureDevOps.Events;
using Confluent.Kafka;
using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Commands;
using CloudEngineering.CodeOps.Abstractions.Events;
using CloudEngineering.CodeOps.Abstractions.Facade;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.EventConsumer.Strategies
{
    public sealed class VstsWebHookConsumptionStrategy : ConsumtionStrategy
    {
        public VstsWebHookConsumptionStrategy(IMapper mapper, IFacade applicationFacade) : base(mapper, applicationFacade)
        {

        }

        public override Task Apply(ConsumeResult<string, string> target, CancellationToken cancellationToken)
        {
            var payload = target.Message.Value;

            if (!string.IsNullOrEmpty(payload))
            {
                var @event = JsonSerializer.Deserialize<IntegrationEvent>(payload);
                var webHookEvent = _mapper.Map<WebHookEvent>(@event.Payload.Value);
                var adoDto = _mapper.Map<AdoDto>(webHookEvent);
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
