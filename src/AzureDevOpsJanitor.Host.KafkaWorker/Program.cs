using AzureDevOpsJanitor.Host.KafkaWorker.Handlers;
using Dafda.Configuration;
using Microsoft.Extensions.Hosting;

namespace AzureDevOpsJanitor.Host.KafkaWorker
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {                    
                services.AddConsumer(options =>
                {
                    options.WithConfigurationSource(hostContext.Configuration);

                    options.RegisterMessageHandler<SampleMessage, SampleMessageHandler>("TOPIC", "MESSAGE_TYPE");
                });
            });
    }
}
