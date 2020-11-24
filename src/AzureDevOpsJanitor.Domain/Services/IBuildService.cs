using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.ValueObjects;
using ResourceProvisioning.Abstractions.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Domain.Services
{
	public interface IBuildService : IDomainService
	{
		Task<IEnumerable<BuildRoot>> GetBuildsAsync();

		Task<BuildRoot> GetBuildByIdAsync(int buildId);

		Task<BuildRoot> AddBuildAsync(string capabilityId, BuildDefinition definition, CancellationToken cancellationToken = default);

		Task DeleteBuildAsync(int buildId, CancellationToken cancellationToken = default);
	}
}
