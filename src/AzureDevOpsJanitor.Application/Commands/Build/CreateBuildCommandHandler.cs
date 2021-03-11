using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Services;
using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
    public sealed class CreateBuildCommandHandler : ICommandHandler<CreateBuildCommand, BuildRoot>, ICommandHandler<CreateBuildCommand, IAggregateRoot>
    {
        private readonly IBuildService _buildService;

        public CreateBuildCommandHandler(IBuildService buildService)
        {
            _buildService = buildService ?? throw new ArgumentNullException(nameof(buildService));
        }

        public async Task<BuildRoot> Handle(CreateBuildCommand command, CancellationToken cancellationToken = default)
        {
            var build = await _buildService.AddAsync(command.ProjectId, command.CapabilityId, command.BuildDefinition, command.Tags, cancellationToken);

            await _buildService.QueueAsync(build.Id, cancellationToken);

            return build;
        }

        async Task<IAggregateRoot> ICommandHandler<CreateBuildCommand, IAggregateRoot>.Handle(CreateBuildCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }

        async Task<IAggregateRoot> IRequestHandler<CreateBuildCommand, IAggregateRoot>.Handle(CreateBuildCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }
    }
}
