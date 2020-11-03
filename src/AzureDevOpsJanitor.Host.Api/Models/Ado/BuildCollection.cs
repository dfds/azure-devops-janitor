using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Host.Api.Models.Ado
{
    public class BuildCollection
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("value")]
        public IEnumerable<Build> Items { get; set; }
    }
}
