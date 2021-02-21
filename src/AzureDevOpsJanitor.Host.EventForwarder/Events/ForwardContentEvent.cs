using CloudEngineering.CodeOps.Abstractions.Events;
using System;
using System.Text.Json;

namespace AzureDevOpsJanitor.Host.EventForwarder.Events
{
    public sealed class ForwardContentEvent : IntegrationEvent
    {
        public ForwardContentEvent(Guid id, string type, JsonElement payload, Guid correlationId, DateTime createDate, int schemaVersion = 1) : base(id, type, payload, createDate, correlationId, schemaVersion)
        {
        }
    }
}