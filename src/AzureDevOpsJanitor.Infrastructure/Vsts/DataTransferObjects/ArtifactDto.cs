using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects
{
    public sealed class ArtifactDto : VstsDto
    {
        [JsonPropertyName("alias")]
        public string Alias { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}