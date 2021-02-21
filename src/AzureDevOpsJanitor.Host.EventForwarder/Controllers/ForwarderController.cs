using AzureDevOpsJanitor.Host.EventForwarder.Attributes;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using CloudEngineering.CodeOps.Abstractions.Events;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace AzureDevOpsJanitor.Host.EventForwarder.Controllers
{
    [ApiController]
    [Route("/api/forwarder")]
    //[ApiKeyAuthorize]
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
                    Value = new IntegrationEvent(Guid.NewGuid(), nameof(JsonElement), json)
                };

                await _producer.ProduceAsync(topic, message);
            }

            return new OkResult();
        }
    }
}