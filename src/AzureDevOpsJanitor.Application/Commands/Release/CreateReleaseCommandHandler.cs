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
    public sealed class CreateReleaseCommandHandler : ICommandHandler<CreateReleaseCommand, ReleaseRoot>, ICommandHandler<CreateReleaseCommand, IAggregateRoot>
    {
        private readonly IReleaseService _releaseService;

        public CreateReleaseCommandHandler(IReleaseService releaseService)
        {
            _releaseService = releaseService ?? throw new ArgumentNullException(nameof(releaseService));
        }

        public async Task<ReleaseRoot> Handle(CreateReleaseCommand command, CancellationToken cancellationToken = default)
        {
            var release = await _releaseService.AddAsync(command.Name, cancellationToken);

            return release;
        }

        async Task<IAggregateRoot> ICommandHandler<CreateReleaseCommand, IAggregateRoot>.Handle(CreateReleaseCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }

        async Task<IAggregateRoot> IRequestHandler<CreateReleaseCommand, IAggregateRoot>.Handle(CreateReleaseCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }
    }
}
