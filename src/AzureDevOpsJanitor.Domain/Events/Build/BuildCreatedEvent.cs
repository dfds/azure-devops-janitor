using AzureDevOpsJanitor.Domain.Aggregates.Build;

namespace AzureDevOpsJanitor.Domain.Events.Build
{
    public sealed class BuildCreatedEvent : BuildEvent
    {
        public BuildCreatedEvent(BuildRoot build)
        {
            Build = build;
        }
    }
}
