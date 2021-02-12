using AzureDevOpsJanitor.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace AzureDevOpsJanitor.Application.Data.Configurations
{
    public class BuildEnvironmentEntityTypeConfiguration : IEntityTypeConfiguration<BuildEnvironment>
    {
        private readonly IEnumerable<BuildEnvironment> _seed;

        public BuildEnvironmentEntityTypeConfiguration()
        {
        }

        public BuildEnvironmentEntityTypeConfiguration(IEnumerable<BuildEnvironment> seed)
        {
            _seed = seed;
        }

        public void Configure(EntityTypeBuilder<BuildEnvironment> configuration)
        {
            configuration.ToTable("Environment");
            configuration.HasKey(o => o.Name);
            configuration.Property(o => o.Name).ValueGeneratedNever();

            if (_seed != null)
            {
                configuration.HasData(_seed);
            }
        }
    }
}
