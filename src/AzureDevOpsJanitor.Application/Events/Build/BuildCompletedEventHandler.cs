using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using AzureDevOpsJanitor.Domain.Events.Build;

namespace AzureDevOpsJanitor.Application.Events.Build
{
	public sealed class BuildCompletedEventHandler : IEventHandler<BuildCompletedEvent>
	{
		public Task Handle(BuildCompletedEvent @event, CancellationToken cancellationToken)
		{
			//TODO: Finalize event handler logic
			return Task.CompletedTask;
		}
	}
}
