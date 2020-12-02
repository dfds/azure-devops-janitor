﻿using System;
using System.Net.Http;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Profile
{
    public sealed class GetProjectRequest : BaseRequest
    {
        public GetProjectRequest(string organization) {
            ApiVersion = "6.0";
            Method = HttpMethod.Get;
            RequestUri = new Uri($"https://dev.azure.com/{organization}/_apis/projects?api-version={ApiVersion}");
        }
    }
}
