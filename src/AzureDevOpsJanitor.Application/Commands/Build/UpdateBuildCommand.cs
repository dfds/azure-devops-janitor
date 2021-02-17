using AzureDevOpsJanitor.Domain.Aggregates.Build;
using CloudEngineering.CodeOps.Abstractions.Commands;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
    public sealed class UpdateBuildCommand : ICommand<BuildRoot>
    {
        [JsonPropertyName("build")]
        public BuildRoot Build { get; init; }

        [JsonConstructor]
        public UpdateBuildCommand(BuildRoot build)
        {
            Build = build;
        }
    }
}
