using System.Text.Json.Serialization;

namespace azure_devops_janitor.Models.Ado
{
    public class StsPayload
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}
