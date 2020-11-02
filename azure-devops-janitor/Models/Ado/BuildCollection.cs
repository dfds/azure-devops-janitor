using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace azure_devops_janitor.Models.Ado
{
    public class BuildCollection
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("value")]
        public IEnumerable<Build> Items { get; set; }
    }
}
