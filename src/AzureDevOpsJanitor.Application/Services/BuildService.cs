using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Repository;
using AzureDevOpsJanitor.Domain.Services;
using AzureDevOpsJanitor.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Services
{
    public sealed class BuildService : IBuildService
    {
        private readonly IBuildRepository _buildRepository;

        public BuildService(IBuildRepository buildRepository)
        {
            _buildRepository = buildRepository;
        }

        public async Task<IEnumerable<BuildRoot>> GetAsync()
        {
            return await _buildRepository.GetAsync((build) => true);
        }

        public async Task<BuildRoot> GetAsync(int buildId)
        {
            return await _buildRepository.GetAsync(buildId);
        }

        public async Task<IEnumerable<BuildRoot>> GetAsync(Guid projectId)
        {
            return await _buildRepository.GetAsync(projectId);
        }

        public async Task<BuildRoot> AddAsync(Guid projectId, string capabilityId, BuildDefinition definition, IEnumerable<Tag> tags = default, CancellationToken cancellationToken = default)
        {
            var build = _buildRepository.Add(new BuildRoot(projectId, capabilityId, definition, tags));

            await _buildRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return build;
        }

        public async Task DeleteAsync(int buildId, CancellationToken cancellationToken = default)
        {
            var build = await GetAsync(buildId);

            if (build != null)
            {
                build.Stopped();

                _buildRepository.Delete(build);

                await _buildRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
        }

        public async Task QueueAsync(int buildId, CancellationToken cancellationToken = default)
        {
            var build = await GetAsync(buildId);

            if (build != null)
            {
                build.Queue();

                await _buildRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
        }

        public async Task<BuildRoot> UpdateAsync(BuildRoot build, CancellationToken cancellationToken = default)
        {
            build = _buildRepository.Update(build);

            await _buildRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return build;
        }
    }
}
