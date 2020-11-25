using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Repository;
using AzureDevOpsJanitor.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Services
{
	public sealed class ProjectService : IProjectService
	{
		private readonly IProjectRepository _projectRepository;

		public ProjectService(IProjectRepository projectRepository)
		{
			_projectRepository = projectRepository;
		}

		public async Task<IEnumerable<ProjectRoot>> GetAsync()
		{
			return await _projectRepository.GetAsync((project) => true);
		}

		public async Task<ProjectRoot> GetAsync(Guid projectId)
		{
			return await _projectRepository.GetAsync(projectId);
		}

		public async Task<ProjectRoot> AddAsync(string name, CancellationToken cancellationToken = default)
		{
			var project = _projectRepository.Add(new ProjectRoot(name));

			await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			return project;
		}

		public async Task DeleteAsync(Guid projectId, CancellationToken cancellationToken = default)
		{
			var project = await GetAsync(projectId);

			if (project != null)
			{
				_projectRepository.Delete(project);

				await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
			}
		}
	}
}
