using System;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects.Apis
{
    public sealed class DefinitionReference
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("project")]
        public string Project { get; set; }

        [JsonPropertyName("revision")]
        public string Revision { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("uri")]
        public Uri Uri { get; set; }
    }
}