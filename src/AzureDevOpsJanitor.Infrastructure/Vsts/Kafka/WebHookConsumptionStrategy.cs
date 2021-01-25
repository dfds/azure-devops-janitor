using AutoMapper;
using AzureDevOpsJanitor.Infrastructure.Kafka.Strategies;
using Confluent.Kafka;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Facade;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Kafka
{
    public sealed class WebHookConsumptionStrategy : ConsumtionStrategy
    {
        public WebHookConsumptionStrategy(IMapper mapper, IFacade applicationFacade) : base(mapper, applicationFacade)
        {
        }

        public override ValueTask<ConsumeResult<string, string>> Apply(ConsumeResult<string, string> target, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(target.Message.Value))
            {
                //TODO: Rewire this -
                //1) Deserialize string to integration event
                //2) Extract aggregate from envelope
                //3) Map agg to cmd
                //var payload = target.Message.Value;
                //IAggregateRoot aggregate;

                ////TODO: Finish switch or create a automapper type converter that can figure out how to convert JsonElement to any dto.
                //switch (@event.Type)
                //{
                //    case nameof(JsonElement):
                //    default:
                //        var jsonElement = JsonSerializer.Deserialize<JsonElement>(payload.GetString());

                //        aggregate = _mapper.Map<IAggregateRoot>(jsonElement);
                //        break;
                //}

                //var command = _mapper.Map<IAggregateRoot, ICommand<IAggregateRoot>>(aggregate);

                //if (command != null)
                //{
                //    _applicationFacade.Execute(command, cancellationToken);
                //}
            }

            return new ValueTask<ConsumeResult<string, string>>(target);
        }
    }
}
