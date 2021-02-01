using AzureDevOpsJanitor.Domain.Aggregates.Release;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework.Configurations
{
    public class ReleaseRootEntityTypeConfiguration : IEntityTypeConfiguration<ReleaseRoot>
    {
        private readonly IEnumerable<ReleaseRoot> _seed;

        public ReleaseRootEntityTypeConfiguration()
        {
        }

        public ReleaseRootEntityTypeConfiguration(IEnumerable<ReleaseRoot> seed)
        {
            _seed = seed;
        }

        public void Configure(EntityTypeBuilder<ReleaseRoot> configuration)
        {
            configuration.ToTable("Release");
            configuration.HasKey(o => o.Id);
            configuration.Ignore(b => b.DomainEvents);

            if (_seed != null)
            {
                configuration.HasData(_seed);
            }
        }
    }
}
