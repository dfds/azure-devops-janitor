using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using System;
using System.Net.Http;
using System.Text.Json;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build
{
    public sealed class QueueBuildRequest : BaseRequest
    {
        public QueueBuildRequest(string organization, string project, DefinitionReference definition) : this(organization, project, definition.Id)
        {
            Content = new StringContent(JsonSerializer.Serialize(definition));
        }

        public QueueBuildRequest(string organization, string project, int definitionId)
        {
            ApiVersion = "6.0";
            Method = HttpMethod.Post;
            RequestUri = new Uri($"https://dev.azure.com/{organization}/{project}/_apis/build/builds?api-version={ApiVersion}&definitionId={definitionId}");
        }
    }
}
