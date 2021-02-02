using AzureDevOpsJanitor.Domain.Aggregates.Release;
using ResourceProvisioning.Abstractions.Commands;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Release
{
    public sealed class CreateReleaseCommand : ICommand<ReleaseRoot>
    {
        [JsonPropertyName("name")]
        public string Name { get; init; }

        [JsonConstructor]
        public CreateReleaseCommand(string name)
        {
            Name = name;
        }
    }
}
