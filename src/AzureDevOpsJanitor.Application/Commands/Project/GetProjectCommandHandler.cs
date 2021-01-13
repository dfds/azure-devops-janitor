using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Services;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Project
{
    public sealed class GetProjectCommandHandler : ICommandHandler<GetProjectCommand, IEnumerable<ProjectRoot>>
	{
		private readonly IProjectService _projectService;

		public GetProjectCommandHandler(IProjectService projectService)
		{
			_projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
		}

		public async Task<IEnumerable<ProjectRoot>> Handle(GetProjectCommand command, CancellationToken cancellationToken = default)
		{
			IEnumerable<ProjectRoot> result;

			if (command.ProjectId.HasValue)
			{
				result = new List<ProjectRoot>() { await _projectService.GetAsync(command.ProjectId.Value) };
			}
			else
			{
				result = await _projectService.GetAsync();
			}
			
			return result;
		}
	}
}
