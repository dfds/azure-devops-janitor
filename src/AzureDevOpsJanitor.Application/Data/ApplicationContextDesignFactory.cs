using AzureDevOpsJanitor.Application.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework
{
    public sealed class ApplicationContextDesignFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var connection = new SqliteConnection("Filename=:memory:;");

            connection.Open();

            var optionsBuilder = new DbContextOptionsBuilder<EntityContext>()
                .UseSqlite(connection);

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
