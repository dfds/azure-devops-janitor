using AzureDevOpsJanitor.Domain.Aggregates.Build;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
    public sealed class GetBuildCommand : ICommand<IEnumerable<BuildRoot>>
    {
        [JsonPropertyName("buildId")]
        public int? BuildId { get; private set; }

        [JsonPropertyName("projectId")]
        public Guid? ProjectId { get; private set; }

        [JsonConstructor]
        public GetBuildCommand(int? buildId = default, Guid? projectId = default)
        {
            BuildId = buildId;
            ProjectId = projectId;
        }
    }
}
