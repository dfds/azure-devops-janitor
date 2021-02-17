using AutoMapper;
using AzureDevOpsJanitor.Domain.Events.Build;
using AzureDevOpsJanitor.Domain.Services;
using CloudEngineering.CodeOps.Abstractions.Events;
using CloudEngineering.CodeOps.Infrastructure.AzureDevOps;
using CloudEngineering.CodeOps.Infrastructure.AzureDevOps.DataTransferObjects.Build;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Events.Build
{
    public sealed class BuildCreatedEventHandler : IEventHandler<BuildCreatedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IAdoClient _restClient;
        private readonly IProjectService _projectService;

        public BuildCreatedEventHandler(IMapper mapper, IAdoClient restClient, IProjectService projectService)
        {
            _mapper = mapper;
            _restClient = restClient;
            _projectService = projectService;
        }

        public async Task Handle(BuildCreatedEvent @event, CancellationToken cancellationToken = default)
        {
            var buildDto = _mapper.Map<BuildDto>(@event.Build);
            var project = await _projectService.GetAsync(@event.Build.ProjectId);
            
            await _restClient.CreateBuildDefinition(project.Name, buildDto.Definition, cancellationToken: cancellationToken);
        }
    }
}
