using AzureDevOpsJanitor.Domain.Aggregates.Release;
using AzureDevOpsJanitor.Domain.Services;
using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Release
{
    public sealed class UpdateReleaseCommandHandler : ICommandHandler<UpdateReleaseCommand, ReleaseRoot>, ICommandHandler<UpdateReleaseCommand, IAggregateRoot>
    {
        private readonly IReleaseService _releaseService;

        public UpdateReleaseCommandHandler(IReleaseService releaseService)
        {
            _releaseService = releaseService ?? throw new ArgumentNullException(nameof(releaseService));
        }

        public async Task<ReleaseRoot> Handle(UpdateReleaseCommand command, CancellationToken cancellationToken = default)
        {
            var release = await _releaseService.UpdateAsync(command.Release, cancellationToken);

            return release;
        }

        async Task<IAggregateRoot> ICommandHandler<UpdateReleaseCommand, IAggregateRoot>.Handle(UpdateReleaseCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }

        async Task<IAggregateRoot> IRequestHandler<UpdateReleaseCommand, IAggregateRoot>.Handle(UpdateReleaseCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }
    }
}
