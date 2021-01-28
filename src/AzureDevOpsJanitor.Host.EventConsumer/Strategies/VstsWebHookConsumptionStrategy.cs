using AutoMapper;
using AzureDevOpsJanitor.Infrastructure.Kafka.Strategies;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using Confluent.Kafka;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Facade;
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

                //TODO: Implement IntegrationEventToDtoConverter
                //var dto = _mapper.Map<BuildDto>(@event);

                //TODO: Implement DtoToAggregateRootConverter
               // var aggregateRoot = _mapper.Map<IAggregateRoot>(dto);

                //TODO: Finalize AggregateRootToCommandConverter
               // var command = _mapper.Map<IAggregateRoot, ICommand<IAggregateRoot>>(aggregateRoot);

               // if (command != null)
               // {
                //    _applicationFacade.Execute(command, cancellationToken);
                //}
            }

            return Task.CompletedTask;
        }
    }
}
