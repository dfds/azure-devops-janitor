using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using AzureDevOpsJanitor.Domain.Events.Build;

namespace AzureDevOpsJanitor.Application.Events.Build
{
	public sealed class BuildRequestedEventHandler : IEventHandler<BuildRequestedEvent>
	{
		public Task Handle(BuildRequestedEvent @event, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
