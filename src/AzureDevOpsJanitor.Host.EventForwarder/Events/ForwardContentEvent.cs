using ResourceProvisioning.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace AzureDevOpsJanitor.Host.EventForwarder.Events
{
    public class ForwardContentEvent : IntegrationEvent
    {
        public ForwardContentEvent(string type, JsonElement payload, Guid id, Guid correlationId, DateTime createDate, int version = 1, IEnumerable<string> topics = default) : base(type, payload, id, correlationId, createDate, version, topics)
        {
        }
    }
}