using AzureDevOpsJanitor.Host.EventForwarder.Enablers.ApiKey;
using AzureDevOpsJanitor.Infrastructure.Kafka;
using AzureDevOpsJanitor.Infrastructure.Kafka.Events;
using Confluent.Kafka;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ResourceProvisioning.Abstractions.Events;

namespace AzureDevOpsJanitor.Host.EventForwarder
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KafkaOptions>(Configuration.GetSection(KafkaOptions.Kafka));

            services.AddTransient(p =>
            {
                var logger = p.GetService<ILogger<IProducer<Ignore, IIntegrationEvent>>>();
                var producerOptions = p.GetService<KafkaOptions>();
                var producerBuilder = new ProducerBuilder<Ignore, IIntegrationEvent>(producerOptions.Configuration);
                var producer = producerBuilder.SetErrorHandler((_, e) => logger.LogError($"Error: {e.Reason}", e))
                                            .SetStatisticsHandler((_, json) => logger.LogDebug($"Statistics: {json}"))
                                            .Build();

                return producer;
            });

            services.AddTransient<INotificationHandler<IIntegrationEvent>, DefaultIntegrationEventHandler>();
            services.AddTransient<IEventHandler<IIntegrationEvent>, DefaultIntegrationEventHandler>();

            services.AddScoped<IApiKeyService, FileApiKeyService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}