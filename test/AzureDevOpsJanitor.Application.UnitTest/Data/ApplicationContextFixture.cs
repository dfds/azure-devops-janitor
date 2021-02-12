using AzureDevOpsJanitor.Application.Data;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace AzureDevOpsJanitor.Application.UnitTest.Data
{
    public class ApplicationContextFixture : IDisposable
    {
        private readonly DbContextOptions _options;
        private readonly SqliteConnection _connection;

        public ApplicationContextFixture()
        {
            _connection = new SqliteConnection("Filename=:memory:;");

            _connection.Open();

            _options = new DbContextOptionsBuilder().UseSqlite(_connection).Options;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "<Pending>")]
        public void Dispose()
        {
            _connection.Dispose();
        }

        public ApplicationContext GetDbContext(IMediator mediator = default)
        {
            return new ApplicationContext(_options, mediator);
        }
    }
}
