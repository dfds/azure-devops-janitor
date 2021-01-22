using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using System;
using System.Net.Http;
using System.Text.Json;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build.Definition
{
    public sealed class CreateDefinitionRequest : ApiRequest
    {
        public CreateDefinitionRequest(string organization, string project, DefinitionDto definition)
        {
            ApiVersion = "6.1-preview.7";
            Method = HttpMethod.Post;
            RequestUri = new Uri($"https://dev.azure.com/{organization}/{project}/_apis/build/definitions?api-version={ApiVersion}");
            Content = new StringContent(JsonSerializer.Serialize(definition));
        }
    }
}
