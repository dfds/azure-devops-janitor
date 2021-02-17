using AzureDevOpsJanitor.Domain.Aggregates.Release;
using AzureDevOpsJanitor.Domain.Services;
using CloudEngineering.CodeOps.Abstractions.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Release
{
    public sealed class CreateReleaseCommandHandler : ICommandHandler<CreateReleaseCommand, ReleaseRoot>
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
    }
}
