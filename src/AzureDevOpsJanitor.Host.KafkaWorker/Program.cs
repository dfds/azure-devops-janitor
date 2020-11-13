using Dafda.Configuration;
using Microsoft.Extensions.Hosting;

namespace AzureDevOpsJanitor.Host.KafkaWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {                    
                // configure messaging: consumer
                services.AddConsumer(options =>
                {
                    // configuration settings
                    options.WithConfigurationSource(hostContext.Configuration);

                    //TODO: Create message handler
                });
            });
    }
}
