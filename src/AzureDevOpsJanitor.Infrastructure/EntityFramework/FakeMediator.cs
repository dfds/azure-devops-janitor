using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework
{
	public sealed class FakeMediator : IMediator
	{
		public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
		{
			return Task.CompletedTask;
		}

		public Task Publish(object notification, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
		{
			return Task.FromResult(default(TResponse));
		}

		public Task Send(IRequest request, CancellationToken cancellationToken = default)
		{
			return Task.CompletedTask;
		}

		public Task<object> Send(object request, CancellationToken cancellationToken = default)
		{
			return Task.FromResult(default(object));
		}
	}
}
