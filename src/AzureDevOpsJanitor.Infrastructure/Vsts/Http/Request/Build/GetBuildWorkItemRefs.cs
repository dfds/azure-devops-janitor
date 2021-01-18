using System;
using System.Net.Http;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build.Definition
{
    public sealed class GetBuildWorkItemRefs : BaseRequest
    {
        public GetBuildWorkItemRefs(string organization, string project, int buildId) {
            ApiVersion = "6.1-preview.2";
            Method = HttpMethod.Get;
            RequestUri = new Uri($"https://dev.azure.com/{organization}/{project}/_apis/build/builds/{buildId}/workitems?api-version={ApiVersion}");
        }
    }
}
