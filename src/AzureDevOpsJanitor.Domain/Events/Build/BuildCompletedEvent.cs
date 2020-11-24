using AzureDevOpsJanitor.Domain.Aggregates.Build;
using ResourceProvisioning.Abstractions.Events;

namespace AzureDevOpsJanitor.Domain.Events.Build
{
	public sealed class BuildCompletedEvent : IDomainEvent
	{
		public int Id { get; }

		public BuildStatus Status { get; }

		public BuildCompletedEvent(int buildId, BuildStatus status)
		{
			Id = buildId;
			Status = status;
		}
	}
}
