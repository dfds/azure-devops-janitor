using AzureDevOpsJanitor.Domain.Aggregates.Build;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework.Configurations
{
	public class BuildRootEntityTypeConfiguration : IEntityTypeConfiguration<BuildRoot>
	{
		private readonly IEnumerable<BuildRoot> _seed;

		public BuildRootEntityTypeConfiguration()
		{
		}

		public BuildRootEntityTypeConfiguration(IEnumerable<BuildRoot> seed)
		{
			_seed = seed;
		}

		public void Configure(EntityTypeBuilder<BuildRoot> configuration)
		{
			configuration.ToTable("Build");
			configuration.HasKey(o => o.Id);
			configuration.Ignore(b => b.DomainEvents);
			configuration.Property<int>("StatusId").IsRequired();

			configuration.HasOne(o => o.Status)
				.WithMany()
				.HasForeignKey("StatusId");

			if (_seed != null)
			{
				configuration.HasData(_seed);
			}
		}
	}
}
