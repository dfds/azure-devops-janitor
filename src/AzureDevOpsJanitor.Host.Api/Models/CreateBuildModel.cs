using AzureDevOpsJanitor.Domain.ValueObjects;

namespace AzureDevOpsJanitor.Host.Api.Models
{
    public class CreateBuildModel
    {
        public string CapabilityId { get; set; }

        public BuildDefinition Definition { get; set; }
    }
}
