using AzureDevOpsJanitor.Domain.Aggregates.Build;
using ResourceProvisioning.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Domain.Repository
{
    public interface IBuildRepository : IRepository<BuildRoot>
    {
        Task<BuildRoot> GetAsync(int buildId);

        Task<IEnumerable<BuildRoot>> GetAsync(Guid projectId);
    }
}
