using AzureDevOpsJanitor.Domain.Aggregates.Release;
using AzureDevOpsJanitor.Domain.Repository;
using AzureDevOpsJanitor.Infrastructure.EntityFramework;
using AzureDevOpsJanitor.Infrastructure.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Repositories
{
    public sealed class ReleaseRepository : EntityFrameworkRepository<ReleaseRoot>, IReleaseRepository
    {
        public ReleaseRepository(DomainContext context) : base(context)
        {

        }

        public Task<ReleaseRoot> GetAsync(Guid releaseId)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<ReleaseRoot>> GetAsync(Expression<Func<ReleaseRoot, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
