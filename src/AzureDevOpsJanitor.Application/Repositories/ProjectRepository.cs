using AzureDevOpsJanitor.Application.Data;
using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Repository;
using CloudEngineering.CodeOps.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Repositories
{
    public sealed class ProjectRepository : EntityFrameworkRepository<ProjectRoot, ApplicationContext>, IProjectRepository
    {
        public ProjectRepository(ApplicationContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<ProjectRoot>> GetAsync(Expression<Func<ProjectRoot, bool>> filter)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _context.Project
                             .AsNoTracking()
                             .Where(filter)
                             .AsEnumerable();
            });
        }

        public async Task<ProjectRoot> GetAsync(Guid projectId)
        {
            var project = await _context.Project.FindAsync(projectId);

            return project;
        }
    }
}
