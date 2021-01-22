using AutoMapper;
using AzureDevOpsJanitor.Application.Behaviors;
using AzureDevOpsJanitor.Application.Caching;
using AzureDevOpsJanitor.Application.Commands.Build;
using AzureDevOpsJanitor.Application.Commands.Profile;
using AzureDevOpsJanitor.Application.Commands.Project;
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
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Facade;
using ResourceProvisioning.Abstractions.Repositories;
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
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddInfrastructure(configuration);

            //Application dependencies
            services.AddBehaviors();
            services.AddCaching();
            services.AddCommandHandlers();
            services.AddEventHandlers();
            services.AddRepositories();
            services.AddServices();
            services.AddFacade();
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
