using AzureDevOpsJanitor.Domain.Aggregates.Release;
using CloudEngineering.CodeOps.Abstractions.Events;

namespace AzureDevOpsJanitor.Domain.Events.Release
{
    public abstract class ReleaseEvent : IDomainEvent
    {
        public ReleaseRoot Release { get; protected set; }
    }
}
