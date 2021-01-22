using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using System;
using System.Net.Http;
using System.Text.Json;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build
{
    public sealed class UpdateDefinitionRequest : ApiRequest
    {
        public UpdateDefinitionRequest(string organization, string project, DefinitionDto definition) : this(organization, project, definition.Id)
        {
            Content = new StringContent(JsonSerializer.Serialize(definition));
        }

        public UpdateDefinitionRequest(string organization, string project, int definitionId)
        {
            ApiVersion = "6.1-preview.7";
            Method = HttpMethod.Put;
            RequestUri = new Uri($"https://dev.azure.com/{organization}/{project}/_apis/build/builds/definitions/{definitionId}?api-version={ApiVersion}");
        }
    }
}
