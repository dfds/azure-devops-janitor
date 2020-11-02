using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace azure_devops_janitor.Models.Ado
{
    public class ProjectCollection
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("value")]
        public IEnumerable<Project> Items { get; set; }
    }
}
