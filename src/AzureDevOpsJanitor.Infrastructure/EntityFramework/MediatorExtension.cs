using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ResourceProvisioning.Abstractions.Entities;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework
{
	static class MediatorExtension
	{
		public static async Task DispatchDomainEventsAsync<T>(this IMediator mediator, T ctx) where T : DbContext
		{
			var domainEntities = ctx.ChangeTracker
				.Entries<IEntity>()
				.Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

			var domainEvents = domainEntities
				.SelectMany(x => x.Entity.DomainEvents)
				.ToList();

			domainEntities.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());

			var tasks = domainEvents
				.Select(async (domainEvent) =>
				{
					await mediator.Publish(domainEvent);
				});

			await Task.WhenAll(tasks);
		}
	}
}
