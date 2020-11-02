using AzureDevOpsJanitor.Domain.Aggregates.Build;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework.Configurations
{
	class BuildStatusEntityTypeConfiguration : IEntityTypeConfiguration<BuildStatus>
	{
		public void Configure(EntityTypeBuilder<BuildStatus> configuration)
		{
			configuration.ToTable("Status");
			configuration.HasKey(o => o.Id);

			configuration.Property(o => o.Id)
				.ValueGeneratedNever()
				.IsRequired();

			configuration.Property(o => o.Name)
				.HasMaxLength(200)
				.IsRequired();
		}
	}
}
