﻿using ResourceProvisioning.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Events
{
    public abstract class WebHookEvent : IIntegrationEvent
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }

        [JsonPropertyName("publisherId")]
        public string PublisherId { get; init; }

        [JsonPropertyName("eventType")]
        public string EventType { get; init; }

        [JsonPropertyName("scope")]
        public string Scope { get; init; }

        [JsonPropertyName("message")]
        public JsonElement? Message { get; init; }

        [JsonPropertyName("resource")]
        public JsonElement? Resource { get; init; }

        public Guid CorrelationId => Id;

        public DateTime CreationDate => DateTime.Now;

        public int SchemaVersion => 1;

        public string Type => EventType;

        public JsonElement? Payload => Message;

        public IEnumerable<string> Topics { get; init; }
    }
}
