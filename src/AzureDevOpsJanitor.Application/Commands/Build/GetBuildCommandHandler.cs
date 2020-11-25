using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Services;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
	public sealed class GetBuildCommandHandler : CommandHandler<GetBuildCommand, IEnumerable<BuildRoot>>
	{
		private readonly IBuildService _buildService;

		public GetBuildCommandHandler(IMediator mediator, IBuildService controlPlaneService) : base(mediator)
		{
			_buildService = controlPlaneService ?? throw new ArgumentNullException(nameof(controlPlaneService));
		}

		public override async Task<IEnumerable<BuildRoot>> Handle(GetBuildCommand command, CancellationToken cancellationToken = default)
		{
			IEnumerable<BuildRoot> result;

			if (command.BuildId.HasValue)
			{
				result = new List<BuildRoot>() { await _buildService.GetAsync(command.BuildId.Value) };
			}
			else
			{
				result = await _buildService.GetAsync();
			}
			
			return result;
		}
	}
}
