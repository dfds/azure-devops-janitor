using AzureDevOpsJanitor.Domain.Aggregates.Project;
using CloudEngineering.CodeOps.Abstractions.Events;

namespace AzureDevOpsJanitor.Domain.Events.Project
{
    public abstract class ProjectEvent : IDomainEvent
    {
        public ProjectRoot Project { get; protected set; }
    }
}
