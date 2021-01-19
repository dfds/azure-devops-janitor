using AzureDevOpsJanitor.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework.Configurations
{
    public class BuildDefinitionEntityTypeConfiguration : IEntityTypeConfiguration<BuildDefinition>
    {
        private readonly IEnumerable<BuildDefinition> _seed;

        public BuildDefinitionEntityTypeConfiguration()
        {
        }

        public BuildDefinitionEntityTypeConfiguration(IEnumerable<BuildDefinition> seed)
        {
            _seed = seed;
        }

        public void Configure(EntityTypeBuilder<BuildDefinition> configuration)
        {
            configuration.ToTable("BuildDefinition");
            configuration.HasKey(o => o.Name);
            configuration.Property(o => o.Name).ValueGeneratedNever();

            if (_seed != null)
            {
                configuration.HasData(_seed);
            }
        }
    }
}
