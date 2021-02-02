using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.ValueObjects;
using ResourceProvisioning.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Domain.Services
{
    public interface IBuildService : IService
    {
        Task<IEnumerable<BuildRoot>> GetAsync();

        Task<BuildRoot> GetAsync(int buildId);

        Task<IEnumerable<BuildRoot>> GetAsync(Guid projectId);

        Task<BuildRoot> AddAsync(Guid projectId, string capabilityIdentifier, BuildDefinition definition, IEnumerable<Tag> tags = default, CancellationToken cancellationToken = default);

        Task<BuildRoot> UpdateAsync(BuildRoot build, CancellationToken cancellationToken = default);

        Task DeleteAsync(int buildId, CancellationToken cancellationToken = default);

        Task QueueAsync(int buildId, CancellationToken cancellationToken = default);
    }
}
