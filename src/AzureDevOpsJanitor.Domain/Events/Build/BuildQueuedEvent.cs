using AzureDevOpsJanitor.Domain.Aggregates.Build;

namespace AzureDevOpsJanitor.Domain.Events.Build
{
	public sealed class BuildQueuedEvent : BuildEvent
	{
		public BuildQueuedEvent(BuildRoot build)
		{
			Build = build;
		}
	}
}
