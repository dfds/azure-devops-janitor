using Microsoft.EntityFrameworkCore;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Repositories;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework.Repositories
{
	public abstract class EntityFrameworkRepository<TAggregate> : Repository<DomainContext, TAggregate>
		where TAggregate : class, IAggregateRoot
	{
		protected EntityFrameworkRepository(DomainContext context) : base(context)
		{

		}

		public override TAggregate Add(TAggregate aggregate)
		{
			return _context.Add(aggregate).Entity;
		}

		public override TAggregate Update(TAggregate aggregate)
		{
			var changeTracker = _context.Entry(aggregate);

			changeTracker.State = EntityState.Modified;

			return changeTracker.Entity;
		}

		public override void Delete(TAggregate aggregate)
		{
			_context.Entry(aggregate).State = EntityState.Deleted;
		}
	}
}
