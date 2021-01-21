using ResourceProvisioning.Abstractions.Events;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Events
{
    public abstract class WebHookEvent : IIntegrationEvent
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
        public JsonElement? Message { get; set; }

        [JsonPropertyName("resource")]
        public JsonElement? Resource { get; set; }

        public Guid CorrelationId => Id;

        public DateTime CreationDate => DateTime.Now;

        public int SchemaVersion => 1;

        public string Type => EventType;

        public JsonElement? Payload => Message;
    }
}
