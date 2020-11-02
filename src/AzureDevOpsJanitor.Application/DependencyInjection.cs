using AutoMapper;
using AzureDevOpsJanitor.Application.Behaviors;
using AzureDevOpsJanitor.Application.Commands.Build;
using AzureDevOpsJanitor.Application.Events.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Events.Build;
using AzureDevOpsJanitor.Domain.Repository;
using AzureDevOpsJanitor.Domain.Services;
using AzureDevOpsJanitor.Infrastructure.EntityFramework;
using AzureDevOpsJanitor.Infrastructure.Repositories;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Data;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Abstractions.Repositories;
using System;
using System.Reflection;

namespace ResourceProvisioning.Broker.Application
{
	public static class DependencyInjection
	{
		public static void AddProvisioningBroker(this IServiceCollection services, Action<ApplicationFacadeOptions> configureOptions = default)
		{
			var options = new ApplicationFacadeOptions();

			configureOptions?.Invoke(options);

			services.AddLogging();
			services.AddOptions<ApplicationFacadeOptions>()
					.Configure(configureOptions);
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddMediator();
			services.AddCommandHandlers();
			services.AddEventHandlers();
			services.AddPersistancy(options);
			services.AddRepositories();
			services.AddServices();
		}

		private static void AddMediator(this IServiceCollection services)
		{
			services.AddTransient<ServiceFactory>(p => p.GetService);
			services.AddTransient<IMediator>(p => new Mediator(p.GetService<ServiceFactory>()));
			
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

			services.AddTransient<IRequestHandler<GetBuildCommand, IProvisioningResponse>, GetBuildCommandHandler>();
			services.AddTransient<IRequestHandler<CreateBuildCommand, IProvisioningResponse>, CreateBuildCommandHandler>();
			services.AddTransient<IRequestHandler<DeleteBuildCommand, IProvisioningResponse>, DeleteBuildCommandHandler>();

			services.AddTransient<INotificationHandler<BuildRequestedEvent>, BuildRequestedEventHandler>();
			services.AddTransient<INotificationHandler<BuildInitializedEvent>, BuildInitializedEventHandler>();
			services.AddTransient<INotificationHandler<BuildCompletedEvent>, BuildCompletedEventHandler>();
		}

		private static void AddCommandHandlers(this IServiceCollection services)
		{
			services.AddTransient<ICommandHandler<GetBuildCommand, IProvisioningResponse>, GetBuildCommandHandler>();
			services.AddTransient<ICommandHandler<CreateBuildCommand, IProvisioningResponse>, CreateBuildCommandHandler>();
			services.AddTransient<ICommandHandler<DeleteBuildCommand, IProvisioningResponse>, DeleteBuildCommandHandler>();
			services.AddTransient<ICommandHandler<IProvisioningRequest, IProvisioningResponse>>(factory => factory.GetRequiredService<IProvisioningBroker>());
		}

		private static void AddEventHandlers(this IServiceCollection services)
		{
			services.AddTransient<IEventHandler<BuildRequestedEvent>, BuildRequestedEventHandler>();
			services.AddTransient<IEventHandler<BuildInitializedEvent>, BuildInitializedEventHandler>();
			services.AddTransient<IEventHandler<BuildCompletedEvent>, BuildCompletedEventHandler>();
			services.AddTransient<IEventHandler<IProvisioningEvent>>(factory => factory.GetRequiredService<IProvisioningBroker>());
		}

		private static void AddPersistancy(this IServiceCollection services, ApplicationFacadeOptions brokerOptions = default)
		{
			services.AddDbContext<DomainContext>(options =>
			{
				var callingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
				var connectionString = brokerOptions?.ConnectionStrings?.GetValue<string>(nameof(DomainContext));

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

				using var context = new DomainContext(dbOptions, new FakeMediator());

				if (!context.Database.EnsureCreated())
				{
					return;
				}

				if (brokerOptions.EnableAutoMigrations)
				{ 
					context.Database.Migrate();
				}
			});

			services.AddScoped<IUnitOfWork>(factory => factory.GetRequiredService<DomainContext>());
		}

		private static void AddRepositories(this IServiceCollection services)
		{
			services.AddTransient<IRepository<BuildRoot>, BuildRepository>();
			services.AddTransient<IBuildRepository, BuildRepository>();
		}

		private static void AddServices(this IServiceCollection services)
		{
			services.AddTransient<IControlPlaneService, ControlPlaneService>();
		}

		private static void AddBroker(this IServiceCollection services)
		{
			services.AddTransient<IProvisioningBroker, ApplicationFacade>();
		}
	}
}
