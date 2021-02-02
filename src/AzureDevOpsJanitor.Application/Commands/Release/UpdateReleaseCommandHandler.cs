using AzureDevOpsJanitor.Domain.Aggregates.Release;
using AzureDevOpsJanitor.Domain.Services;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Release
{
    public sealed class UpdateReleaseCommandHandler : ICommandHandler<UpdateReleaseCommand, ReleaseRoot>
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
    }
}
