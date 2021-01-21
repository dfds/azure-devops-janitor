using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Services;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
    public sealed class UpdateBuildCommandHandler : ICommandHandler<UpdateBuildCommand, BuildRoot>
    {
        private readonly IBuildService _buildService;

        public UpdateBuildCommandHandler(IBuildService buildService)
        {
            _buildService = buildService ?? throw new ArgumentNullException(nameof(buildService));
        }

        public async Task<BuildRoot> Handle(UpdateBuildCommand command, CancellationToken cancellationToken = default)
        {
            return await _buildService.UpdateAsync(command.Build);
        }
    }
}
