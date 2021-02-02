using AzureDevOpsJanitor.Domain.Aggregates.Build;
using ResourceProvisioning.Abstractions.Commands;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
    //TODO: Updated commands to not simply wrap a aggregate root instance. Rather deconstruct it and accept only the props we want to allow people to update
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
