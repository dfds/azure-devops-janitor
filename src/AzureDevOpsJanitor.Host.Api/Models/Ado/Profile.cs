using System;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Host.Api.Models.Ado
{
    public class Profile
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
