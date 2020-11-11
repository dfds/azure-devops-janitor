using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AzureDevOpsJanitor.Host.EventForwarder.Controllers
{
    [ApiController]
    [Route("/api/forwarder")]
    public class ForwarderController : Controller
    {
        private readonly ILogger<ForwarderController> _logger;

        public ForwarderController(ILogger<ForwarderController> logger)
        {
            _logger = logger;
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
                Console.WriteLine(content);
            }
            
            return new OkResult();
        }
    }
}