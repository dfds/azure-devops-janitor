using AzureDevOpsJanitor.Host.EventForwarder.Attributes;
using AzureDevOpsJanitor.Host.EventForwarder.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMediator _mediator;

        public ForwarderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Forward([FromHeader(Name = "x-topic")] string topic)
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var content = await reader.ReadToEndAsync();
                var @event = new ForwardContent(JsonDocument.Parse(content).RootElement, new[] { topic });                

                await _mediator.Publish(@event);
            }

            return new OkResult();
        }
    }
}