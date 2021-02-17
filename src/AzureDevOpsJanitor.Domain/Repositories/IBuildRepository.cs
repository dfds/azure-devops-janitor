using AzureDevOpsJanitor.Domain.Aggregates.Build;
using CloudEngineering.CodeOps.Abstractions.Repositories;
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
