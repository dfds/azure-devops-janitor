using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Aggregates.Release;
using AzureDevOpsJanitor.Domain.ValueObjects;
using CloudEngineering.CodeOps.Abstractions.Data;
using CloudEngineering.CodeOps.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AzureDevOpsJanitor.Application.Data
{
    public class ApplicationContext : EntityContext
    {
        public DbSet<BuildEnvironment> Environments { get; set; }

        public DbSet<Artifact> Artifacts { get; set; }

        public DbSet<ReleaseRoot> Release { get; set; }

        public DbSet<ProjectRoot> Project { get; set; }

        public DbSet<BuildRoot> Build { get; set; }

        public DbSet<BuildStatus> BuildStatus { get; set; }

        public DbSet<BuildDefinition> BuildDefinition { get; set; }

        public ApplicationContext()
        { }

        public ApplicationContext(DbContextOptions options, IMediator mediator = default, IDictionary<Type, IEnumerable<IView>> seedData = default) : base(options)
        {

        }
    }
}
