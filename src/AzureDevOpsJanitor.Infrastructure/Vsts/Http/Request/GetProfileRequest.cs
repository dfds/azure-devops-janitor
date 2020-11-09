using System;
using System.Net.Http;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request
{
    public class GetProfileRequest : HttpRequestMessage
    {
        public GetProfileRequest(string profileId) {
            Method = HttpMethod.Get;
            RequestUri = new Uri($"https://app.vssps.visualstudio.com/_apis/profile/profiles/{profileId}?api-version=6.1-preview.3");
        }
    }
}
