﻿using System;
using System.Net.Http;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build.Definition
{
    public sealed class DeleteBuildRequest : BaseRequest
    {
        public DeleteBuildRequest(string organization, string project, int buildId) {
            ApiVersion = "6.1-preview.6";
            Method = HttpMethod.Delete;
            RequestUri = new Uri($"https://dev.azure.com/{organization}/{project}/_apis/build/{buildId}?api-version={ApiVersion}");
        }
    }
}