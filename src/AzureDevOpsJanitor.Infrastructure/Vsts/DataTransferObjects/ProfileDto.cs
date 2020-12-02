using System;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects
{
    public sealed class ProfileDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("displayName")]
        public string Name { get; set; }

        [JsonPropertyName("publicAlias")]
        public Guid Alias { get; set; }

        [JsonPropertyName("emailAddress")]
        public string Email { get; set; }
    }
}