using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.ValueObjects;
using AzureDevOpsJanitor.Infrastructure.EntityFramework.Configurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ResourceProvisioning.Abstractions.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework
{
	public sealed class DomainContext : DbContext, IUnitOfWork
	{
		public const string DEFAULT_SCHEMA = nameof(DomainContext);
		private readonly IMediator _mediator;

		public DbSet<ProjectRoot> Project { get; set; }

		public DbSet<BuildRoot> Build { get; set; }
		
		public DbSet<BuildStatus> BuildStatus { get; set; }

		public DbSet<BuildDefinition> BuildDefinition { get; set; }

		public IDbContextTransaction GetCurrentTransaction { get; private set; }

		public DomainContext() : this(new DbContextOptions<DomainContext>() { }, new FakeMediator()) { }

		public DomainContext(DbContextOptions options, IMediator mediator) : base(options)
		{
			_mediator = mediator;

			System.Diagnostics.Debug.WriteLine($"{nameof(DomainContext)}::ctor ->" + GetHashCode());
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ProjectEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new BuildDefinitionEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new BuildRootEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new BuildStatusEntityTypeConfiguration());
		}

		public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
		{
			// Dispatch Domain Events collection. 
			// Choices:
			// A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
			// side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
			// B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
			// You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
			await _mediator?.DispatchDomainEventsAsync(this);

			// After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
			// performed through the DbContext will be committed
			await base.SaveChangesAsync(cancellationToken);

			return true;
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var recordsToValidate = ChangeTracker.Entries();

			foreach (var recordToValidate in recordsToValidate)
			{
				var entity = recordToValidate.Entity;
				var validationContext = new ValidationContext(entity);
				var results = new List<ValidationResult>();

				if (!Validator.TryValidateObject(entity, validationContext, results, true))
				{
					var messages = results.Select(r => r.ErrorMessage).ToList().Aggregate((message, nextMessage) => message + ", " + nextMessage);

					throw new System.Exception($"Unable to save changes for {entity.GetType().FullName} due to error(s): {messages}");
				}
			}

			return await base.SaveChangesAsync(cancellationToken);
		}

		public async Task BeginTransactionAsync()
		{
			GetCurrentTransaction = GetCurrentTransaction ?? await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
		}

		public async Task CommitTransactionAsync()
		{
			try
			{
				await SaveEntitiesAsync();

				GetCurrentTransaction?.Commit();
			}
			catch
			{
				RollbackTransaction();

				throw;
			}
			finally
			{
				if (GetCurrentTransaction != null)
				{
					GetCurrentTransaction.Dispose();

					GetCurrentTransaction = null;
				}
			}
		}

		public void RollbackTransaction()
		{
			try
			{
				GetCurrentTransaction?.Rollback();
			}
			finally
			{
				if (GetCurrentTransaction != null)
				{
					GetCurrentTransaction.Dispose();

					GetCurrentTransaction = null;
				}
			}
		}
	}
}
