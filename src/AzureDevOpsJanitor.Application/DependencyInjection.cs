using AzureDevOpsJanitor.Application.Behaviors;
using AzureDevOpsJanitor.Application.Caching;
using AzureDevOpsJanitor.Application.Commands.Build;
using AzureDevOpsJanitor.Application.Commands.Profile;
using AzureDevOpsJanitor.Application.Commands.Project;
using AzureDevOpsJanitor.Application.Data;
using AzureDevOpsJanitor.Application.Events.Build;
using AzureDevOpsJanitor.Application.Repositories;
using AzureDevOpsJanitor.Application.Services;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Events.Build;
using AzureDevOpsJanitor.Domain.Repository;
using AzureDevOpsJanitor.Domain.Services;
using AzureDevOpsJanitor.Domain.ValueObjects;
using AzureDevOpsJanitor.Infrastructure;
using CloudEngineering.CodeOps.Abstractions.Commands;
using CloudEngineering.CodeOps.Abstractions.Data;
using CloudEngineering.CodeOps.Abstractions.Events;
using CloudEngineering.CodeOps.Abstractions.Facade;
using CloudEngineering.CodeOps.Abstractions.Repositories;
using CloudEngineering.CodeOps.Infrastructure;
using CloudEngineering.CodeOps.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
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
            services.AddApplicationContext(configuration);
            services.AddBehaviors();
            services.AddCaching();
            services.AddCommandHandlers();
            services.AddEventHandlers();
            services.AddFacade();
            services.AddRepositories();
            services.AddServices();
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

        private static void AddBehaviors(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        }

        private static void AddCaching(this IServiceCollection services)
        {
            services.AddSingleton<IMemoryCache, ApplicationCache>();
        }

        private static void AddCommandHandlers(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<GetBuildCommand, IEnumerable<BuildRoot>>, GetBuildCommandHandler>();
            services.AddTransient<IRequestHandler<CreateBuildCommand, BuildRoot>, CreateBuildCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateBuildCommand, BuildRoot>, UpdateBuildCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteBuildCommand, bool>, DeleteBuildCommandHandler>();
            services.AddTransient<IRequestHandler<GetProfileCommand, UserProfile>, GetProfileCommandHandler>();
            services.AddTransient<IRequestHandler<GetProjectCommand, IEnumerable<ProjectRoot>>, GetProjectCommandHandler>();
            services.AddTransient<IRequestHandler<CreateProjectCommand, ProjectRoot>, CreateProjectCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateProjectCommand, ProjectRoot>, UpdateProjectCommandHandler>();

            services.AddTransient<ICommandHandler<GetBuildCommand, IEnumerable<BuildRoot>>, GetBuildCommandHandler>();
            services.AddTransient<ICommandHandler<CreateBuildCommand, BuildRoot>, CreateBuildCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateBuildCommand, BuildRoot>, UpdateBuildCommandHandler>();
            services.AddTransient<ICommandHandler<DeleteBuildCommand, bool>, DeleteBuildCommandHandler>();
            services.AddTransient<ICommandHandler<GetProfileCommand, UserProfile>, GetProfileCommandHandler>();
            services.AddTransient<ICommandHandler<GetProjectCommand, IEnumerable<ProjectRoot>>, GetProjectCommandHandler>();
            services.AddTransient<ICommandHandler<CreateProjectCommand, ProjectRoot>, CreateProjectCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateProjectCommand, ProjectRoot>, UpdateProjectCommandHandler>();
        }

        private static void AddEventHandlers(this IServiceCollection services)
        {
            services.AddTransient<INotificationHandler<BuildCreatedEvent>, BuildCreatedEventHandler>();
            services.AddTransient<INotificationHandler<BuildQueuedEvent>, BuildQueuedEventHandler>();

            services.AddTransient<IEventHandler<BuildCreatedEvent>, BuildCreatedEventHandler>();
            services.AddTransient<IEventHandler<BuildQueuedEvent>, BuildQueuedEventHandler>();
        }

        private static void AddFacade(this IServiceCollection services)
        {
            services.AddTransient<IFacade, ApplicationFacade>();
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

    }
}
