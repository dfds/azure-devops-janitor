using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Aggregates.Release;
using AzureDevOpsJanitor.Domain.Repository;
using AzureDevOpsJanitor.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Services
{
    public sealed class ReleaseService : IReleaseService
    {
        private readonly IReleaseRepository _releaseRepository;

        public ReleaseService(IReleaseRepository releaseRepository)
        {
            _releaseRepository = releaseRepository;
        }

        public async Task<IEnumerable<ReleaseRoot>> GetAsync()
        {
            return await _releaseRepository.GetAsync((release) => true);
        }

        public async Task<ReleaseRoot> GetAsync(int releaseId)
        {
            var result = await _releaseRepository.GetAsync(r => r.Id == releaseId);

            return result.SingleOrDefault();
        }

        public async Task<ReleaseRoot> AddAsync(string name, CancellationToken cancellationToken = default)
        {
            var release = _releaseRepository.Add(new ReleaseRoot(name));

            await _releaseRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return release;
        }

        public async Task DeleteAsync(int releaseId, CancellationToken cancellationToken = default)
        {
            var release = await GetAsync(releaseId);

            if (release != null)
            {
                _releaseRepository.Delete(release);

                await _releaseRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<ReleaseRoot> UpdateAsync(ReleaseRoot release, CancellationToken cancellationToken = default)
        {
            release = _releaseRepository.Update(release);

            await _releaseRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return release;
        }
    }
}
