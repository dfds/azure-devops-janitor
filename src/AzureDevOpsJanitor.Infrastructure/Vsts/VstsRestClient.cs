using AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Vsts
{
    public class VstsRestClient : HttpClient, IVstsRestClient
    {
        public const string VstsAccessTokenCacheKey = "vstsAccessToken";

        public VstsRestClient(JwtSecurityToken accessToken)
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken.RawData);
        }

        public async Task<VstsProfile> GetProfile(string profileId)
        {
            var response = await SendAsync(new GetProfileRequest(profileId));
            var responseData = await response.Content.ReadAsStringAsync();
            var profileDto = JsonSerializer.Deserialize<VstsProfile>(responseData);

            return profileDto;
        }
    }
}
