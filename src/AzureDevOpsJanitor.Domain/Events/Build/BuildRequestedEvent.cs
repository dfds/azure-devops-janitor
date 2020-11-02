using AzureDevOpsJanitor.Domain.Aggregates.Build;
using ResourceProvisioning.Abstractions.Events;

namespace AzureDevOpsJanitor.Domain.Events.Build
{
	public sealed class BuildRequestedEvent : IPivotEvent
	{
		public BuildRoot Build { get; }

		public BuildRequestedEvent(BuildRoot build)
		{
			Build = build;
		}
	}
}
