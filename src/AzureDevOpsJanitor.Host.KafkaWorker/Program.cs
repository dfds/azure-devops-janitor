using AzureDevOpsJanitor.Host.KafkaWorker.Handlers;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects.Events;
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

        //TODO: Replace Dafda with vanilla consumer @ https://github.com/dfds/dojo/blob/master/workshops/kafka-deep-dive/4/final/Enablers/KafkaConsumer.cs
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {                    
                services.AddConsumer(options =>
                {
                    options.WithConfigurationSource(hostContext.Configuration);

                    options.RegisterMessageHandler<BuildCompletedEvent, BuildCompletedEventHandler>("pub.segment-ui-beorp.default", "my-message-key");
                });
            });
    }
}
