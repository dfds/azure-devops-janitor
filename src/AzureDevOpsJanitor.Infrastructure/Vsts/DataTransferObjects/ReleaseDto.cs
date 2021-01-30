using System;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects
{
    //TODO: Finalize DTO based on MS documentation
    public sealed class ReleaseDto : VstsDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("url")]
        public Uri Url { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("definition")]
        public ReleaseDefinitionDto Definition { get; set; }
    }
}
