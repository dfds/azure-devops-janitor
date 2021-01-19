using AzureDevOpsJanitor.Domain.Aggregates.Project;
using ResourceProvisioning.Abstractions.Commands;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Project
{
    public sealed class CreateProjectCommand : ICommand<ProjectRoot>
	{
		[JsonPropertyName("name")]
		public string Name { get; init; }

		[JsonConstructor]
		public CreateProjectCommand(string name)
		{
			Name = name;
		}
	}
}
