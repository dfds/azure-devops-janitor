using System;
using System.Text.Json.Serialization;

namespace azure_devops_janitor.Models.Ado
{
    public class Profile
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
