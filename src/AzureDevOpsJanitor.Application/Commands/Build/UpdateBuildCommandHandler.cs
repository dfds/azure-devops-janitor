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
    public sealed class UpdateBuildCommandHandler : ICommandHandler<UpdateBuildCommand, BuildRoot>, ICommandHandler<UpdateBuildCommand, IAggregateRoot>
    {
        private readonly IBuildService _buildService;

        public UpdateBuildCommandHandler(IBuildService buildService)
        {
            _buildService = buildService ?? throw new ArgumentNullException(nameof(buildService));
        }

        public async Task<BuildRoot> Handle(UpdateBuildCommand command, CancellationToken cancellationToken = default)
        {
            return await _buildService.UpdateAsync(command.Build, cancellationToken);
        }

        async Task<IAggregateRoot> ICommandHandler<UpdateBuildCommand, IAggregateRoot>.Handle(UpdateBuildCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }

        async Task<IAggregateRoot> IRequestHandler<UpdateBuildCommand, IAggregateRoot>.Handle(UpdateBuildCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }
    }
}
