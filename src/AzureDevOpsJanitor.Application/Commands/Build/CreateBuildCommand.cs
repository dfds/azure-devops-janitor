using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.ValueObjects;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
    public sealed class CreateBuildCommand : ICommand<BuildRoot>
    {
        [JsonPropertyName("projectId")]
        public Guid ProjectId { get; init; }

        [JsonPropertyName("capabilityId")]
        public string CapabilityId { get; init; }

        [JsonPropertyName("buildDefinition")]
        public BuildDefinition BuildDefinition { get; init; }

        [JsonConstructor]
        public CreateBuildCommand(Guid projectId, string capabilityId, BuildDefinition buildDefinition)
        {
            ProjectId = projectId;
            CapabilityId = capabilityId;
            BuildDefinition = buildDefinition;
        }
    }
}
