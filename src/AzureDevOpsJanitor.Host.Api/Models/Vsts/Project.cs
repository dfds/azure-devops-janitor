using System;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Host.Api.Models.Vsts
{
    public class Project
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("url")]
        public Uri Uri { get; set; }
    }
}
