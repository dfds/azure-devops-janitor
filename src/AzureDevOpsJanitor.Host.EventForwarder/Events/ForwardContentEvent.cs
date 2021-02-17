using CloudEngineering.CodeOps.Abstractions.Events;
using System;
using System.Text.Json;

namespace AzureDevOpsJanitor.Host.EventForwarder.Events
{
    public sealed class ForwardContentEvent : IntegrationEvent
    {
        public ForwardContentEvent(string type, JsonElement payload, Guid id, Guid correlationId, DateTime createDate, int schemaVersion = 1)
        {
            Type = type;
            Payload = payload;
            Id = id;
            CorrelationId = correlationId;
            CreationDate = createDate;
            SchemaVersion = schemaVersion;
        }
    }
}