using AzureDevOpsJanitor.Domain.Aggregates.Project;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Project
{
    public sealed class GetProjectCommand : ICommand<IEnumerable<ProjectRoot>>
    {
        [JsonPropertyName("projectId")]
        public Guid? ProjectId { get; private set; }

        [JsonConstructor]
        public GetProjectCommand(Guid? projectId = default)
        {
            ProjectId = projectId;
        }
    }
}
