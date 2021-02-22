using CloudEngineering.CodeOps.Abstractions.Events;
using CloudEngineering.CodeOps.Infrastructure.AzureDevOps;
using CloudEngineering.CodeOps.Infrastructure.EntityFramework;
using CloudEngineering.CodeOps.Infrastructure.Kafka;
using CloudEngineering.CodeOps.Infrastructure.Kafka.Serialization;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CloudEngineering.CodeOps.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediator();
            services.AddEntityFramework(configuration);
            services.AddAzureDevOps(configuration);
            services.AddKafka(configuration);
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddTransient<ServiceFactory>(p => p.GetService);

            services.AddTransient<IMediator>(p => new Mediator(p.GetService<ServiceFactory>()));
        }

        private static void AddAzureDevOps(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AdoClient).Assembly);

            services.Configure<AdoClientOptions>(configuration.GetSection(AdoClientOptions.AdoClient));

            services.AddTransient<IAdoClient, AdoClient>();
        }

        private static void AddKafka(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<KafkaOptions>(configuration.GetSection(KafkaOptions.Kafka));

            services.AddTransient(p =>
            {
                var logger = p.GetService<ILogger<IProducer<string, IIntegrationEvent>>>();
                var producerOptions = p.GetService<IOptions<KafkaOptions>>();
                var producerBuilder = new ProducerBuilder<string, IIntegrationEvent>(producerOptions.Value.Configuration);
                var producer = producerBuilder.SetErrorHandler((_, e) => logger.LogError($"Error: {e?.Reason}", e))
                                            .SetStatisticsHandler((_, json) => logger.LogDebug($"Statistics: {json}"))
                                            .SetValueSerializer(new DefaultValueSerializer<IIntegrationEvent>())
                                            .Build();

                return producer;
            });
        }

        private static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EntityContextOptions>(configuration);
        }
    }
}
