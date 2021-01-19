using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework
{
	public sealed class DomainDesignFactory : IDesignTimeDbContextFactory<DomainContext>
	{
		public DomainContext CreateDbContext(string[] args)
		{
			var connection = new SqliteConnection("Filename=:memory:;");

			connection.Open();

			var optionsBuilder = new DbContextOptionsBuilder<DomainContext>()
				.UseSqlite(connection);

			return new DomainContext(optionsBuilder.Options);
		}
	}
}
