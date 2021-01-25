using AzureDevOpsJanitor.Infrastructure.EntityFramework;
using AzureDevOpsJanitor.Infrastructure.Kafka;
using AzureDevOpsJanitor.Infrastructure.Kafka.Serialization;
using AzureDevOpsJanitor.Infrastructure.Vsts;
using Confluent.Kafka;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Abstractions.Data;
using ResourceProvisioning.Abstractions.Events;
using System.Reflection;

namespace AzureDevOpsJanitor.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediator();
            services.AddEntityFramework(configuration);
            services.AddVsts(configuration);
            services.AddKafka(configuration);
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddTransient<ServiceFactory>(p => p.GetService);

            services.AddTransient<IMediator>(p => new Mediator(p.GetService<ServiceFactory>()));
        }

        private static void AddVsts(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<VstsClientOptions>(configuration.GetSection(VstsClientOptions.Vsts));

            services.AddTransient<IVstsClient, VstsClient>();
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
            services.Configure<DomainContextOptions>(configuration);

            services.AddDbContext<DomainContext>(options =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var dbContextOptions = serviceProvider.GetService<IOptions<DomainContextOptions>>();
                var callingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                var connectionString = dbContextOptions.Value.ConnectionStrings?.GetValue<string>(nameof(DomainContext));

                if (string.IsNullOrEmpty(connectionString))
                {
                    return;
                }

                services.AddSingleton(factory =>
                {
                    var connection = new SqliteConnection(connectionString);

                    connection.Open();

                    return connection;
                });

                var dbOptions = options.UseSqlite(services.BuildServiceProvider().GetService<SqliteConnection>(),
                    sqliteOptions =>
                    {
                        sqliteOptions.MigrationsAssembly(callingAssemblyName);
                        sqliteOptions.MigrationsHistoryTable(callingAssemblyName + "_MigrationHistory");

                    }).Options;

                using var context = new DomainContext(dbOptions, serviceProvider.GetService<IMediator>());

                if (!context.Database.EnsureCreated())
                {
                    return;
                }

                if (dbContextOptions.Value.EnableAutoMigrations)
                {
                    context.Database.Migrate();
                }
            });

            services.AddScoped<IUnitOfWork>(factory => factory.GetRequiredService<DomainContext>());
        }
    }
}
