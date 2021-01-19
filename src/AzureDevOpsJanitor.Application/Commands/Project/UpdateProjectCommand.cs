using AzureDevOpsJanitor.Domain.Aggregates.Project;
using ResourceProvisioning.Abstractions.Commands;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Project
{
    public sealed class UpdateProjectCommand : ICommand<ProjectRoot>
	{
		[JsonPropertyName("project")]
		public ProjectRoot Project { get; init; }

		[JsonConstructor]
		public UpdateProjectCommand(ProjectRoot project)
		{
			Project = project;
		}
	}
}
