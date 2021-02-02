using AzureDevOpsJanitor.Domain.Aggregates.Release;
using AzureDevOpsJanitor.Domain.Repository;
using AzureDevOpsJanitor.Infrastructure.EntityFramework;
using AzureDevOpsJanitor.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Repositories
{
    public sealed class ReleaseRepository : EntityFrameworkRepository<ReleaseRoot>, IReleaseRepository
    {
        public ReleaseRepository(DomainContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<ReleaseRoot>> GetAsync(Expression<Func<ReleaseRoot, bool>> filter)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _context.Release
                             .AsNoTracking()
                             .Where(filter)
                             .AsEnumerable();
            });
        }

        public async Task<ReleaseRoot> GetAsync(Guid releaseId)
        {
            var project = await _context.Release.FindAsync(releaseId);

            return project;
        }
    }
}
