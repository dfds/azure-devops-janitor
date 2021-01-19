using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Services;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Project
{
    public sealed class UpdateProjectCommandHandler : ICommandHandler<UpdateProjectCommand, ProjectRoot>
    {
        private readonly IProjectService _projectService;

        public UpdateProjectCommandHandler(IProjectService projectService)
        {
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
        }

        public async Task<ProjectRoot> Handle(UpdateProjectCommand command, CancellationToken cancellationToken = default)
        {
            var project = await _projectService.UpdateAsync(command.Project, cancellationToken);

            return project;
        }
    }
}
