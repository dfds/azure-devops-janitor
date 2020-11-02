using AzureDevOpsJanitor.Domain.Aggregates.Build;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework.Configurations
{
	class BuildRootEntityTypeConfiguration : IEntityTypeConfiguration<BuildRoot>
	{
		public void Configure(EntityTypeBuilder<BuildRoot> configuration)
		{
			configuration.ToTable("Build");
			configuration.HasKey(o => o.Id);
			configuration.Ignore(b => b.DomainEvents);
			configuration.Property<int>("StatusId").IsRequired();

			configuration.HasOne(o => o.Status)
				.WithMany()
				.HasForeignKey("StatusId");
		}
	}
}
