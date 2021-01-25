using AutoMapper;
using Confluent.Kafka;
using ResourceProvisioning.Abstractions.Facade;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Kafka.Strategies
{
    public sealed class DefaultConsumptionStrategy : ConsumtionStrategy
    {
        public DefaultConsumptionStrategy(IMapper mapper, IFacade applicationFacade) : base(mapper, applicationFacade)
        {
        }

        public override ValueTask<ConsumeResult<string, string>> Apply(ConsumeResult<string, string> target, CancellationToken cancellationToken = default)
        {
            return new ValueTask<ConsumeResult<string, string>>(target);
        }
    }
}
