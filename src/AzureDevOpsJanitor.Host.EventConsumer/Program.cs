using AzureDevOpsJanitor.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AzureDevOpsJanitor.Host.EventConsumer
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
            services.AddApplication(hostContext.Configuration);
            services.AddHostedService<AdoEventWorker>();
        })
        .ConfigureLogging(logBuilder =>
        {
            logBuilder.AddSentry();
        });
    }
}