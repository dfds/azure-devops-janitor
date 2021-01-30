using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Aggregates.Release;
using ResourceProvisioning.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Domain.Services
{
    public interface IReleaseService : IService
    {
        Task<IEnumerable<ReleaseRoot>> GetAsync();

        Task<ReleaseRoot> GetAsync(Guid releaseId);

        Task<ReleaseRoot> AddAsync(string name, CancellationToken cancellationToken = default);

        Task<ReleaseRoot> UpdateAsync(ProjectRoot project, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid releaseId, CancellationToken cancellationToken = default);
    }
}
