using AzureDevOpsJanitor.Domain.Aggregates.Release;
using ResourceProvisioning.Abstractions.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Domain.Services
{
    public interface IReleaseService : IService
    {
        Task<IEnumerable<ReleaseRoot>> GetAsync();

        Task<ReleaseRoot> GetAsync(int releaseId);

        Task<ReleaseRoot> AddAsync(string name, CancellationToken cancellationToken = default);

        Task<ReleaseRoot> UpdateAsync(ReleaseRoot release, CancellationToken cancellationToken = default);

        Task DeleteAsync(int releaseId, CancellationToken cancellationToken = default);
    }
}
