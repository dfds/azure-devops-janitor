using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Repository;
using AzureDevOpsJanitor.Infrastructure.EntityFramework;
using AzureDevOpsJanitor.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AzureDevOpsJanitor.Application.Data;

namespace AzureDevOpsJanitor.Application.Repositories
{
    public sealed class BuildRepository : EntityFrameworkRepository<BuildRoot, ApplicationContext>, IBuildRepository
    {
        public BuildRepository(ApplicationContext context) : base(context)
        {

        }

        public override Task<IEnumerable<BuildRoot>> GetAsync(Expression<Func<BuildRoot, bool>> filter)
        {
            return Task.FromResult(_context.Build
                        .AsNoTracking()
                        .Where(filter)
                        .Include(i => i.Status)
                        .Include(i => i.Definition)
                        .AsEnumerable());
        }

        public async Task<BuildRoot> GetAsync(int buildId)
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

        public async Task<IEnumerable<BuildRoot>> GetAsync(Guid projectId)
        {
            return await GetAsync(o => o.ProjectId == projectId);
        }
    }
}
