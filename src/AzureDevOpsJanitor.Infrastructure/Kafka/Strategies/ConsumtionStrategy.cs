using AutoMapper;
using Confluent.Kafka;
using ResourceProvisioning.Abstractions.Strategies;
using ResourceProvisioning.Abstractions.Facade;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Kafka.Strategies
{
    public abstract class ConsumtionStrategy : IStrategy<ConsumeResult<Ignore, string>>
    {
        protected readonly IFacade _applicationFacade;
        protected readonly IMapper _mapper;

        protected ConsumtionStrategy(IMapper mapper, IFacade applicationFacade)
        {
            _mapper = mapper ?? throw new ArgumentException(null, nameof(mapper));
            _applicationFacade = applicationFacade ?? throw new ArgumentException(null, nameof(applicationFacade));
        }

        public abstract ValueTask<ConsumeResult<Ignore, string>> Apply(ConsumeResult<Ignore, string> target, CancellationToken cancellationToken);
    }
}
