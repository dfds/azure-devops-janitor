﻿using System;
using System.Net.Http;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build.Definition
{
    public sealed class GetDefinitionYamlRequest : BaseRequest
    {
        public GetDefinitionYamlRequest(string organization, string project, int definitionId) {
            ApiVersion = "6.1-preview.7";
            Method = HttpMethod.Get;
            RequestUri = new Uri($"https://dev.azure.com/{organization}/{project}/_apis/build/definitions/{definitionId}?dummyValue=1234&api-version={ApiVersion}");
        }
    }
}
