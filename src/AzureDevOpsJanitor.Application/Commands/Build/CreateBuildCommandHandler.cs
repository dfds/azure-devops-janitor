using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Services;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
    public sealed class CreateBuildCommandHandler : ICommandHandler<CreateBuildCommand, BuildRoot>
	{
		private readonly IBuildService _buildService;

		public CreateBuildCommandHandler(IBuildService buildService)
		{
			_buildService = buildService ?? throw new ArgumentNullException(nameof(buildService));
		}

		public async Task<BuildRoot> Handle(CreateBuildCommand command, CancellationToken cancellationToken = default)
		{
			var build = await _buildService.AddAsync(command.ProjectId, command.CapabilityId, command.BuildDefinition, cancellationToken);

			await _buildService.QueueAsync(build.Id, cancellationToken);

			return build;
		}
	}
}
