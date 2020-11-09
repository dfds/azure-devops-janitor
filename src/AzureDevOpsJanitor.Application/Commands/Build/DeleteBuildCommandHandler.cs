using AzureDevOpsJanitor.Domain.Services;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
	public sealed class DeleteBuildCommandHandler : CommandHandler<DeleteBuildCommand, bool>
	{
		private readonly IBuildService _buildService;

		public DeleteBuildCommandHandler(IMediator mediator, IBuildService buildService) : base(mediator)
		{
			_buildService = buildService ?? throw new ArgumentNullException(nameof(buildService));
		}

		public override async Task<bool> Handle(DeleteBuildCommand command, CancellationToken cancellationToken = default)
		{
			if (command.BuildId > 0)
			{
				try
				{
					await _buildService.DeleteBuildAsync(command.BuildId, cancellationToken);
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
