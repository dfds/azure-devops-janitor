using ResourceProvisioning.Abstractions.Events;

namespace AzureDevOpsJanitor.Domain.Events.Build
{
	public sealed class BuildCreatedEvent : IDomainEvent
	{
		public int BuildId { get; }

		public BuildCreatedEvent(int buildId)
		{
			BuildId = buildId;
		}
	}
}
