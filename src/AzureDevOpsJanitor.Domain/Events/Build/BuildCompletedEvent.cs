using AzureDevOpsJanitor.Domain.Aggregates.Build;

namespace AzureDevOpsJanitor.Domain.Events.Build
{
	public sealed class BuildCompletedEvent : BuildEvent
	{
		public BuildCompletedEvent(BuildRoot build)
		{
			Build = build;
		}
	}
}
