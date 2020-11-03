using AzureDevOpsJanitor.Domain.Aggregates.Build;
using ResourceProvisioning.Abstractions.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Domain.Services
{
	public interface IBuildService : IDomainService
	{
		Task<IEnumerable<BuildRoot>> GetBuildsAsync();

		Task<BuildRoot> GetBuildByIdAsync(ulong buildId);

		Task<BuildRoot> AddBuildAsync(string capabilityId, CancellationToken cancellationToken = default);

		Task DeleteBuildAsync(ulong buildId, CancellationToken cancellationToken = default);
	}
}
