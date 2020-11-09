using ResourceProvisioning.Abstractions.Events;

namespace AzureDevOpsJanitor.Domain.Events.Build
{
	public sealed class BuildInitializedEvent : IDomainEvent
	{
		public ulong BuildId { get; }

		public BuildInitializedEvent(ulong buildId)
		{
			BuildId = buildId;
		}
	}
}
