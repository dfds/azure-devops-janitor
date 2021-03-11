using AzureDevOpsJanitor.Application.Caching;
using AzureDevOpsJanitor.Application.Data;
using AzureDevOpsJanitor.Application.Repositories;
using AzureDevOpsJanitor.Application.Services;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Repository;
using AzureDevOpsJanitor.Domain.Services;
using AzureDevOpsJanitor.Infrastructure;
using CloudEngineering.CodeOps.Abstractions.Data;
using CloudEngineering.CodeOps.Abstractions.Facade;
using CloudEngineering.CodeOps.Abstractions.Repositories;
using CloudEngineering.CodeOps.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace AzureDevOpsJanitor.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            //Framework dependencies
            services.AddLogging();

            //External dependencies
            services.AddInfrastructure(configuration);

            //Application dependencies
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddApplicationContext(configuration);
            services.AddCaching();
            services.AddRepositories();
            services.AddServices();
            services.AddFacade();
        }

        private static void AddApplicationContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EntityContextOptions>(configuration);

            services.AddDbContext<ApplicationContext>(options =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var dbContextOptions = serviceProvider.GetService<IOptions<EntityContextOptions>>();
                var callingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                var connectionString = dbContextOptions.Value.ConnectionStrings?.GetValue<string>(nameof(ApplicationContext));

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ApplicationFacadeException($"Could not find connection string with entry key: {nameof(ApplicationContext)}");
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

                using var context = new ApplicationContext(dbOptions, serviceProvider.GetService<IMediator>());

                if (dbContextOptions.Value.EnableAutoMigrations)
                {
                    context.Database.Migrate();
                }
            });

            services.AddScoped<IUnitOfWork>(factory => factory.GetRequiredService<ApplicationContext>());
        }

        private static void AddCaching(this IServiceCollection services)
        {
            services.AddSingleton<IMemoryCache, ApplicationCache>();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository<BuildRoot>, BuildRepository>();
            services.AddTransient<IBuildRepository, BuildRepository>();

            services.AddTransient<IRepository<ProjectRoot>, ProjectRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IBuildService, BuildService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IProfileService, ProfileService>();
        }

        private static void AddFacade(this IServiceCollection services)
        {
            services.AddTransient<IFacade, ApplicationFacade>();
        }
    }
}
