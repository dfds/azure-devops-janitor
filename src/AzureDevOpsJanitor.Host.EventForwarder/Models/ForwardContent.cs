using ResourceProvisioning.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace AzureDevOpsJanitor.Host.EventForwarder.Models
{
    public class ForwardContent : IntegrationEvent
    {
        public IEnumerable<string> Topics { get; init; }

        public ForwardContent(JsonElement payload, IEnumerable<string> topics) : base("forward", payload, Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, 1)
        {
            Topics = topics;
            Payload = payload;
        }
    }
}