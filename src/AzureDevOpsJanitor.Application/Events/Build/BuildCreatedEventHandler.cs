using AutoMapper;
using AzureDevOpsJanitor.Domain.Events.Build;
using AzureDevOpsJanitor.Domain.Services;
using AzureDevOpsJanitor.Infrastructure.Vsts;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using ResourceProvisioning.Abstractions.Events;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Events.Build
{
    public sealed class BuildCreatedEventHandler : IEventHandler<BuildCreatedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IVstsClient _restClient;
        private readonly IProjectService _projectService;

        public BuildCreatedEventHandler(IMapper mapper, IVstsClient restClient, IProjectService projectService)
        {
            _mapper = mapper;
            _restClient = restClient;
            _projectService = projectService;
        }

        public async Task Handle(BuildCreatedEvent @event, CancellationToken cancellationToken = default)
        {
            var buildDef = _mapper.Map<BuildDefinitionDto>(@event.Build);
            var project = await _projectService.GetAsync(@event.Build.ProjectId);

            await _restClient.CreateDefinition(project.Name, buildDef, cancellationToken: cancellationToken);
        }
    }
}
