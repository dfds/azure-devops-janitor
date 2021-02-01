﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects
{
    public sealed class ReleaseDefinitionDto : VstsDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("path")]
        public int Path { get; set; }

        [JsonPropertyName("projectReference")]
        public ProjectDto Project { get; set; }

        [JsonPropertyName("tags")]
        public IEnumerable<string> Tags { get; set; }

        [JsonPropertyName("artifacts")]
        public IEnumerable<ArtifactDto> Artifacts { get; set; }

        [JsonPropertyName("environments")]
        public IEnumerable<EnvironmentDto> Environments { get; set; }
    }
}
