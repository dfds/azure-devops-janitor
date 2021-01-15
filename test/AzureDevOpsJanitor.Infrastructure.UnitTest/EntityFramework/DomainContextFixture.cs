using AzureDevOpsJanitor.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace AzureDevOpsJanitor.Infrastructure.UnitTest.EntityFramework
{
    public class DomainContextFixture : IDisposable
    {
        private readonly DbContextOptions _options;
        private readonly SqliteConnection _connection;

        public DomainContextFixture()
        {
            _connection = new SqliteConnection("Filename=:memory:;");

            _connection.Open();

            _options = new DbContextOptionsBuilder().UseSqlite(_connection).Options;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public DomainContext GetDbContext(IMediator mediator = default)
        {
            return new DomainContext(_options, mediator);
        }
    }
}
