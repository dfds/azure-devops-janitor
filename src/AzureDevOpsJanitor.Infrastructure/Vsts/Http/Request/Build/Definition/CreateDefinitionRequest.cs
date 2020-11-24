using System;
using System.Net.Http;
using System.Text.Json;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build.Definition
{
    public sealed class CreateDefinitionRequest : BaseRequest
    {
        public CreateDefinitionRequest(string organization, string project, DataTransferObjects.DefinitionReference definition) {
            ApiVersion = "6.1-preview.7";
            Method = HttpMethod.Post;
            RequestUri = new Uri($"https://dev.azure.com/{organization}/{project}/_apis/build/definitions?api-version={ApiVersion}");
            Content = new StringContent(JsonSerializer.Serialize(definition));
        }
    }
}
