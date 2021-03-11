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
    public sealed class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand, ProjectRoot>, ICommandHandler<CreateProjectCommand, IAggregateRoot>
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

        async Task<IAggregateRoot> ICommandHandler<CreateProjectCommand, IAggregateRoot>.Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }

        async Task<IAggregateRoot> IRequestHandler<CreateProjectCommand, IAggregateRoot>.Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }
    }
}
