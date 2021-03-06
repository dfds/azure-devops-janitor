﻿using AzureDevOpsJanitor.Domain.Aggregates.Release;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace AzureDevOpsJanitor.Application.Data.Configurations
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

            configuration.HasMany(o => o.Artifacts)
                         .WithMany("Artifacts");

            configuration.HasMany(o => o.Environments)
                         .WithMany("Environments");

            if (_seed != null)
            {
                configuration.HasData(_seed);
            }
        }
    }
}
