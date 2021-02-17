using AzureDevOpsJanitor.Domain.Aggregates.Project;
using CloudEngineering.CodeOps.Abstractions.Repositories;
using System;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Domain.Repository
{
    public interface IProjectRepository : IRepository<ProjectRoot>
    {
        Task<ProjectRoot> GetAsync(Guid projectId);
    }
}
