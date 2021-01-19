﻿using System;
using System.Net.Http;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build.Definition
{
    public sealed class GetBuildRequest : BaseRequest
    {
        public GetBuildRequest(string organization, string project, int buildId) {
            ApiVersion = "6.1-preview.6";
            Method = HttpMethod.Get;
            RequestUri = new Uri($"https://dev.azure.com/{organization}/{project}/_apis/build/{buildId}?api-version={ApiVersion}");
        }
    }
}