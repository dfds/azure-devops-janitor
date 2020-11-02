using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Repository;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Domain.Services
{
	public sealed class ControlPlaneService : IControlPlaneService
	{
		private readonly IBuildRepository _buildRepository;

		public ControlPlaneService(IBuildRepository buildRepository)
		{
			_buildRepository = buildRepository;
		}

		public async Task<IEnumerable<BuildRoot>> GetBuildsAsync()
		{
			return await _buildRepository.GetAsync((build) => true);
		}

		public async Task<BuildRoot> GetBuildByIdAsync(ulong buildId)
		{
			return await _buildRepository.GetByIdAsync(buildId);
		}

		public async Task<BuildRoot> AddBuildAsync(string capabilityId, CancellationToken cancellationToken = default)
		{
			var build = _buildRepository.Add(new BuildRoot(capabilityId));

			await _buildRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			return build;
		}

		public async Task DeleteBuildAsync(ulong buildId, CancellationToken cancellationToken = default)
		{
			var build = await GetBuildByIdAsync(buildId);

			if (build != null)
			{
				_buildRepository.Delete(build);

				await _buildRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
			}
		}
	}
}
