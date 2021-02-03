using AzureDevOpsJanitor.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework.Configurations
{
    public class ArtifactEntityTypeConfiguration : IEntityTypeConfiguration<Artifact>
    {
        private readonly IEnumerable<Artifact> _seed;

        public ArtifactEntityTypeConfiguration()
        {
        }

        public ArtifactEntityTypeConfiguration(IEnumerable<Artifact> seed)
        {
            _seed = seed;
        }

        public void Configure(EntityTypeBuilder<Artifact> configuration)
        {
            configuration.ToTable("Artifact");
            configuration.HasKey(o => o.Name);
            configuration.Property(o => o.Name).ValueGeneratedNever();

            if (_seed != null)
            {
                configuration.HasData(_seed);
            }
        }
    }
}
