using AzureDevOpsJanitor.Domain.Aggregates.Build;
using ResourceProvisioning.Abstractions.Events;

namespace AzureDevOpsJanitor.Domain.Events.Build
{
	public sealed class BuildCompletedEvent : IDomainEvent
	{
		public ulong Id { get; }

		public BuildStatus Status { get; }

		public BuildCompletedEvent(ulong buildId, BuildStatus status)
		{
			Id = buildId;
			Status = status;
		}
	}
}
