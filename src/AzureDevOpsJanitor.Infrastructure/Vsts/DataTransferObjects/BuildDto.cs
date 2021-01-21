using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects
{
    public sealed class BuildDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("project")]
        public TeamProjectDto Project { get; set; }

        [JsonPropertyName("buildNumber")]
        public string BuildNumber { get; set; }

        [JsonPropertyName("uri")]
        public Uri Uri { get; set; }

        [JsonPropertyName("definition")]
        public DefinitionReferenceDto Definition { get; set; }

        [JsonPropertyName("tags")]
        public IEnumerable<string> Tags { get; set; }
    }
}