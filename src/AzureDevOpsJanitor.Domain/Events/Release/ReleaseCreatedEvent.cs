using AzureDevOpsJanitor.Domain.Aggregates.Release;

namespace AzureDevOpsJanitor.Domain.Events.Release
{
    public sealed class ReleaseCreatedEvent : ReleaseEvent
    {
        public ReleaseCreatedEvent(ReleaseRoot release)
        {
            Release = release;
        }
    }
}
