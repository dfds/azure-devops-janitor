using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Host.Api.Models.Vsts
{
    public class ProjectCollection
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("value")]
        public IEnumerable<Project> Items { get; set; }
    }
}
