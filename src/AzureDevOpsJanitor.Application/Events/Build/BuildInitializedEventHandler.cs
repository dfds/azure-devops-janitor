using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using AzureDevOpsJanitor.Domain.Events.Build;

namespace AzureDevOpsJanitor.Application.Events.Build
{
	public sealed class BuildInitializedEventHandler : IEventHandler<BuildInitializedEvent>
	{
		public Task Handle(BuildInitializedEvent @event, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
