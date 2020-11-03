using System;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Host.Api.Models.Ado
{
    public class Build
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("result")]
        public string Result { get; set; }
        
        [JsonPropertyName("uri")]
        public Uri Url { get; set; }

        public bool IsSuccesful 
        {
            get
            {
                return Status == "completed" && Result == "succeeded";
            }
        }
    }
}
