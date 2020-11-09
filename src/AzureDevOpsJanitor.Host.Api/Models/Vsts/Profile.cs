using System;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Host.Api.Models.Vsts
{
    public class Profile
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
