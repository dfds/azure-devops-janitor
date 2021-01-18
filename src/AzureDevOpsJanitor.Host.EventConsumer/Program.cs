using AzureDevOpsJanitor.Application;
using AzureDevOpsJanitor.Infrastructure.Kafka;
using Microsoft.Extensions.DependencyInjection;
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
                DependencyInjection.AddApplication(services);

                services.AddOptions<KafkaOptions>()
                        .Bind(hostContext.Configuration);

                services.AddHostedService<Worker>();
            });
    }
}