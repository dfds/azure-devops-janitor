using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework.Transactions
{
	public sealed class ResilientTransaction
	{
		private readonly DbContext _context;

		private ResilientTransaction(DbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

		public static ResilientTransaction New(DbContext context) => new ResilientTransaction(context);

		public async Task ExecuteAsync(Func<Task> action)
		{
			var strategy = _context.Database.CreateExecutionStrategy();

			await strategy.ExecuteAsync(async () =>
			{
				using var transaction = _context.Database.BeginTransaction();

				await action();

				transaction.Commit();
			});
		}
	}
}
