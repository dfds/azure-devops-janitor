using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Services;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
	public sealed class GetBuildCommandHandler : CommandHandler<GetBuildCommand, IEnumerable<BuildRoot>>
	{
		private readonly IBuildService _buildService;

		public GetBuildCommandHandler(IMediator mediator, IBuildService buildService) : base(mediator)
		{
			_buildService = buildService ?? throw new ArgumentNullException(nameof(buildService));
		}

		public override async Task<IEnumerable<BuildRoot>> Handle(GetBuildCommand command, CancellationToken cancellationToken = default)
		{
			IEnumerable<BuildRoot> result;

			if (command.ProjectId.HasValue)
			{
				result = await _buildService.GetAsync(command.ProjectId.Value);
				
				if (command.BuildId.HasValue)
				{
					result = result.Where(b => b.Id == command.BuildId.Value);
				}
			}
			else if (command.BuildId.HasValue)
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
