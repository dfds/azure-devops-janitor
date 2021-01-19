using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using System;
using System.Net.Http;
using System.Text.Json;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Project
{
    public sealed class UpdateProjectRequest : BaseRequest
    {
        public UpdateProjectRequest(string organization, TeamProjectDto project) : this(organization, project.Id.ToString())
        {
            Content = new StringContent(JsonSerializer.Serialize(project));
        }

        public UpdateProjectRequest(string organization, string projectId) {
            ApiVersion = "6.0";
            Method = HttpMethod.Patch;
            RequestUri = new Uri($"https://dev.azure.com/{organization}/_apis/projects/{projectId}?api-version={ApiVersion}");
        }
    }
}
