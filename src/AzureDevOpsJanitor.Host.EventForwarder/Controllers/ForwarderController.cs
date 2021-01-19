using AzureDevOpsJanitor.Host.EventForwarder.Attributes;
using AzureDevOpsJanitor.Host.EventForwarder.Models;
using AzureDevOpsJanitor.Host.EventForwarder.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.EventForwarder.Controllers
{
    [ApiController]
    [Route("/api/forwarder")]
    [ApiKeyAuthorize]
    public class ForwarderController : Controller
    {
        private readonly ILogger<ForwarderController> _logger;
        private readonly KafkaService _kafkaService;

        public ForwarderController(ILogger<ForwarderController> logger, KafkaService kafkaService)
        {
            _logger = logger;
            _kafkaService = kafkaService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Forward([FromHeader(Name = "x-topic")] string topic, [FromHeader(Name = "x-apiKey")] string apiKey)
        {
            //TODO: Rewrite headers to be a middleware
            _logger.LogInformation($"topic: {topic}");
            _logger.LogInformation($"apiKey: {apiKey}");

            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var content = await reader.ReadToEndAsync();

                _kafkaService.Queue(new ForwardContent()
                {
                    Topic = topic,
                    Payload = content
                });
            }
            
            return new OkResult();
        }
    }
}