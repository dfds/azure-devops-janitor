using AutoMapper;
using AzureDevOpsJanitor.Domain.Events.Build;
using AzureDevOpsJanitor.Infrastructure.Vsts;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using ResourceProvisioning.Abstractions.Events;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Events.Build
{
    public sealed class BuildCreatedEventHandler : IEventHandler<BuildCreatedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IVstsClient _restClient;

        public BuildCreatedEventHandler(IMapper mapper, IVstsClient restClient)
        {
            _mapper = mapper;
            _restClient = restClient;
        }

        public async Task Handle(BuildCreatedEvent @event, CancellationToken cancellationToken = default)
        {
            var buildDef = _mapper.Map<BuildDefinitionDto>(@event.Build.Definition);

            await _restClient.CreateDefinition("dfds", "CloudEngineering", buildDef);
        }
    }
}
