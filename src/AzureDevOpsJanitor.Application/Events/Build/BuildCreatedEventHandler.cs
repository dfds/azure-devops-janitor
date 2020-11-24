using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using AzureDevOpsJanitor.Domain.Events.Build;

namespace AzureDevOpsJanitor.Application.Events.Build
{
	public sealed class BuildCreatedEventHandler : IEventHandler<BuildCreatedEvent>
	{
		public Task Handle(BuildCreatedEvent @event, CancellationToken cancellationToken)
		{
			//TODO: Create build via ADO REST API

			return Task.CompletedTask;
		}
	}
}
