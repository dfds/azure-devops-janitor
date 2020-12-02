using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects.Events
{
    public abstract class VstsEvent
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("publisherId")]
        public string PublisherId { get; set; }

        [JsonPropertyName("eventType")]
        public string EventType { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("message")]
        public JsonElement Message { get; set; }

        [JsonPropertyName("resource")]
        public JsonElement Resource { get; set; }
    }
}
