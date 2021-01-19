using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Services;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Project
{
    public sealed class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand, ProjectRoot>
    {
        private readonly IProjectService _projectService;

        public CreateProjectCommandHandler(IProjectService projectService)
        {
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
        }

        public async Task<ProjectRoot> Handle(CreateProjectCommand command, CancellationToken cancellationToken = default)
        {
            var project = await _projectService.AddAsync(command.Name, cancellationToken);

            return project;
        }
    }
}
