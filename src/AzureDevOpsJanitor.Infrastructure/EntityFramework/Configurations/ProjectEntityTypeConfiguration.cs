using AzureDevOpsJanitor.Domain.Aggregates.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework.Configurations
{
	class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<ProjectRoot>
	{
		public void Configure(EntityTypeBuilder<ProjectRoot> configuration)
		{
			configuration.ToTable("Project");
			configuration.HasKey(o => o.Id);
			configuration.Ignore(b => b.DomainEvents);
		}
	}
}
