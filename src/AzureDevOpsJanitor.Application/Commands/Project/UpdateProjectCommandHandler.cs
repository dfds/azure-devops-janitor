using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Services;
using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Project
{
    public sealed class UpdateProjectCommandHandler : ICommandHandler<UpdateProjectCommand, ProjectRoot>, ICommandHandler<UpdateProjectCommand, IAggregateRoot>
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

        async Task<IAggregateRoot> ICommandHandler<UpdateProjectCommand, IAggregateRoot>.Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }

        async Task<IAggregateRoot> IRequestHandler<UpdateProjectCommand, IAggregateRoot>.Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }
    }
}
