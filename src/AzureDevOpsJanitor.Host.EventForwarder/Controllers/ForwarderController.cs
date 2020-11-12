using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AzureDevOpsJanitor.Host.EventForwarder.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AzureDevOpsJanitor.Host.EventForwarder.Controllers
{
    [ApiController]
    [Route("/api/forwarder")]
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
            // Rewrite headers to be a middleware
            Console.WriteLine($"topic: {topic}");
            Console.WriteLine($"apiKey: {apiKey}");

            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var content = await reader.ReadToEndAsync();
                _kafkaService.Queue(content);
            }
            
            return new OkResult();
        }
    }
}