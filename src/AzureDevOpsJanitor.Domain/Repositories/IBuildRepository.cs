using AzureDevOpsJanitor.Domain.Aggregates.Build;
using ResourceProvisioning.Abstractions.Repositories;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Domain.Repository
{
	public interface IBuildRepository : IRepository<BuildRoot>
	{
		Task<BuildRoot> GetByIdAsync(ulong buildId);
	}
}
