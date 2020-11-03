using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Host.Api.Models.Ado
{
    public class StsPayload
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}
