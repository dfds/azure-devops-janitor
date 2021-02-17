using AzureDevOpsJanitor.Domain.Aggregates.Release;
using CloudEngineering.CodeOps.Abstractions.Repositories;
using System;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Domain.Repository
{
    public interface IReleaseRepository : IRepository<ReleaseRoot>
    {
        Task<ReleaseRoot> GetAsync(Guid releaseId);
    }
}
