using AzureDevOpsJanitor.Host.EventForwarder.Attributes;
using AzureDevOpsJanitor.Host.EventForwarder.Events;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using ResourceProvisioning.Abstractions.Events;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.EventForwarder.Controllers
{
    [ApiController]
    [Route("/api/forwarder")]
    [ApiKeyAuthorize]
    public class ForwarderController : Controller
    {
        private readonly IProducer<string, IIntegrationEvent> _producer;

        public ForwarderController(IProducer<string, IIntegrationEvent> producer)
        {
            _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> Forward([FromHeader(Name = "x-dfds-eventforwarder-topic")] string topic)
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var content = await reader.ReadToEndAsync();
                var json = JsonDocument.Parse(content).RootElement;
                var message = new Message<string, IIntegrationEvent>()
                {
                    Value = new ForwardContentEvent(nameof(JsonElement), json, Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, 1)
                };
                
                await _producer.ProduceAsync(topic, message);
            }

            return new OkResult();
        }
    }
}