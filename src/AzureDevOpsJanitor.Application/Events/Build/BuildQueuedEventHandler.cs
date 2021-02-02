using AutoMapper;
using AzureDevOpsJanitor.Domain.Events.Build;
using AzureDevOpsJanitor.Domain.Services;
using AzureDevOpsJanitor.Infrastructure.Vsts;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using ResourceProvisioning.Abstractions.Events;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Events.Build
{
    public sealed class BuildQueuedEventHandler : IEventHandler<BuildQueuedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IVstsClient _restClient;
        private readonly IProjectService _projectService;

        public BuildQueuedEventHandler(IMapper mapper, IVstsClient restClient, IProjectService projectService)
        {
            _mapper = mapper;
            _restClient = restClient;
            _projectService = projectService;
        }
        public async Task Handle(BuildQueuedEvent @event, CancellationToken cancellationToken = default)
        {
            var buildDef = _mapper.Map<BuildDefinitionDto>(@event.Build);
            var project = await _projectService.GetAsync(@event.Build.ProjectId);

            await _restClient.QueueBuild(project.Name, buildDef, cancellationToken: cancellationToken);
        }
    }
}
