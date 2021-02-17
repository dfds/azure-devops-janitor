using AzureDevOpsJanitor.Application.Data;
using AzureDevOpsJanitor.Domain.Aggregates.Release;
using AzureDevOpsJanitor.Domain.Repository;
using CloudEngineering.CodeOps.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Repositories
{
    public sealed class ReleaseRepository : EntityFrameworkRepository<ReleaseRoot, ApplicationContext>, IReleaseRepository
    {
        public ReleaseRepository(ApplicationContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<ReleaseRoot>> GetAsync(Expression<Func<ReleaseRoot, bool>> filter)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _context.Release
                            .AsNoTracking()
                            .Where(filter)
                            .Include(i => i.Artifacts)
                            .Include(i => i.Environments)
                            .AsEnumerable();
            });
        }

        public async Task<ReleaseRoot> GetAsync(Guid releaseId)
        {
            var release = await _context.Release.FindAsync(releaseId);

            if (release != null)
            {
                var entry = _context.Entry(release);

                if (entry != null)
                {
                    await entry.Reference(i => i.Artifacts).LoadAsync();
                    await entry.Reference(i => i.Environments).LoadAsync();
                }
            }

            return release;
        }
    }
}
