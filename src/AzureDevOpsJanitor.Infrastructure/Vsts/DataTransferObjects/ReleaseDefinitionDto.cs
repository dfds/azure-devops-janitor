using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects
{
    //TODO: Finalize DTO based on MS documentation
    public sealed class ReleaseDefinitionDto : VstsDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("tags")]
        public IEnumerable<string> Tags { get; set; }

        [JsonPropertyName("variables")]
        public IEnumerable<string> Variables { get; set; }
    }
}
