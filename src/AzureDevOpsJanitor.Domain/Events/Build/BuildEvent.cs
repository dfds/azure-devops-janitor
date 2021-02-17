using AzureDevOpsJanitor.Domain.Aggregates.Build;
using CloudEngineering.CodeOps.Abstractions.Events;

namespace AzureDevOpsJanitor.Domain.Events.Build
{
    public abstract class BuildEvent : IDomainEvent
    {
        public BuildRoot Build { get; protected set; }
    }
}
