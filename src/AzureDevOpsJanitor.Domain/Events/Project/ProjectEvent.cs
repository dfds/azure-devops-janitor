using AzureDevOpsJanitor.Domain.Aggregates.Project;
using ResourceProvisioning.Abstractions.Events;

namespace AzureDevOpsJanitor.Domain.Events.Project
{
	public abstract class ProjectEvent : IDomainEvent
	{
		public ProjectRoot Project { get; protected set; }
	}
}
