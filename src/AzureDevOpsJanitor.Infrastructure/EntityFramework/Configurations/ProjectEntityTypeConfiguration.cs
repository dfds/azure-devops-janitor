using AzureDevOpsJanitor.Domain.Aggregates.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework.Configurations
{
	public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<ProjectRoot>
	{
		private readonly IEnumerable<ProjectRoot> _seed;

		public ProjectEntityTypeConfiguration()
		{
		}

		public ProjectEntityTypeConfiguration(IEnumerable<ProjectRoot> seed)
        {
			_seed = seed;
		}

		public void Configure(EntityTypeBuilder<ProjectRoot> configuration)
		{
			configuration.ToTable("Project");
			configuration.HasKey(o => o.Id);
			configuration.Ignore(b => b.DomainEvents);

			if (_seed != null)
			{
				configuration.HasData(_seed);
			}
		}
	}
}
