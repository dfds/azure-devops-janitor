﻿using System;
using System.Net.Http;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Release
{
    public sealed class GetReleaseRequest : ApiRequest
    {
        public GetReleaseRequest(string organization, string project, int releaseId)
        {
            ApiVersion = "6.1-preview.8";
            Method = HttpMethod.Get;
            RequestUri = new Uri($"https://vsrm.dev.azure.com/{organization}/{project}/_apis/release/releases/{releaseId}?api-version={ApiVersion}");
        }
    }
}