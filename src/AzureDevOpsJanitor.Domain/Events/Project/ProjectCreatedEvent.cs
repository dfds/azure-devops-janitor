using AzureDevOpsJanitor.Domain.Aggregates.Project;

namespace AzureDevOpsJanitor.Domain.Events.Project
{
    public sealed class ProjectCreatedEvent : ProjectEvent
    {
        public ProjectCreatedEvent(ProjectRoot project)
        {
            Project = project;
        }
    }
}
