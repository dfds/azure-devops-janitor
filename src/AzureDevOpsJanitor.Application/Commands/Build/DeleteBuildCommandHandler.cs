using AzureDevOpsJanitor.Domain.Services;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
    public sealed class DeleteBuildCommandHandler : ICommandHandler<DeleteBuildCommand, bool>
	{
		private readonly IBuildService _buildService;

		public DeleteBuildCommandHandler(IBuildService buildService)
		{
			_buildService = buildService ?? throw new ArgumentNullException(nameof(buildService));
		}

		public async Task<bool> Handle(DeleteBuildCommand command, CancellationToken cancellationToken = default)
		{
			if (command.BuildId > 0)
			{
				try
				{
					await _buildService.DeleteAsync(command.BuildId, cancellationToken);
				}
				catch
				{
					throw;
				}				
			}

			return true;
		}
	}
}
