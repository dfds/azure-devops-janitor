using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Repository;
using AzureDevOpsJanitor.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using ResourceProvisioning.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Repositories
{
	public class BuildRepository : Repository<DomainContext, BuildRoot>, IBuildRepository
	{
		public BuildRepository(DomainContext context) : base(context)
		{

		}

		public override async Task<IEnumerable<BuildRoot>> GetAsync(Expression<Func<BuildRoot, bool>> filter)
		{
			return await Task.Factory.StartNew(() =>
			{
				return _context.Build
							 .AsNoTracking()
							 .Where(filter)
							 .Include(i => i.Status)
							 .AsEnumerable();
			});
		}

		public async Task<BuildRoot> GetByIdAsync(ulong buildId)
		{
			var build = await _context.Build.FindAsync(buildId);

			if (build != null)
			{
				var entry = _context.Entry(build);

				if (entry != null)
				{
					await entry.Reference(i => i.Status).LoadAsync();
				}
			}

			return build;
		}

		public override BuildRoot Add(BuildRoot aggregate)
		{
			return _context.Add(aggregate).Entity;
		}

		public override BuildRoot Update(BuildRoot aggregate)
		{
			var changeTracker = _context.Entry(aggregate);

			changeTracker.State = EntityState.Modified;

			return changeTracker.Entity;
		}

		public override void Delete(BuildRoot aggregate)
		{
			_context.Entry(aggregate).State = EntityState.Deleted;
		}
	}
}
