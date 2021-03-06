﻿using AzureDevOpsJanitor.Domain.Aggregates.Project;
using CloudEngineering.CodeOps.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Domain.Services
{
    public interface IProjectService : IService
    {
        Task<IEnumerable<ProjectRoot>> GetAsync();

        Task<ProjectRoot> GetAsync(Guid projectId);

        Task<ProjectRoot> AddAsync(string name, CancellationToken cancellationToken = default);

        Task<ProjectRoot> UpdateAsync(ProjectRoot project, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid projectId, CancellationToken cancellationToken = default);
    }
}
