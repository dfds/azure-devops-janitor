using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects
{
    public sealed class OperationDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("pluginId")]
        public string PluginId { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}