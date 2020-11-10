using AzureDevOpsJanitor.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework.Configurations
{
	class BuildDefinitionEntityTypeConfiguration : IEntityTypeConfiguration<BuildDefinition>
	{
		public void Configure(EntityTypeBuilder<BuildDefinition> configuration)
		{
			configuration.ToTable("Definition");
			configuration.HasKey(o => o.Name);
			configuration.Property(o => o.Name).ValueGeneratedNever();
		}
	}
}
