using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build.Definition;
using AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Profile;
using System.Collections.Generic;
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

        public async Task<Profile> GetProfile(string profileId)
        {
            var response = await SendAsync(new GetProfileRequest(profileId));
            var responseData = await response.Content.ReadAsStringAsync();
            var profileDto = JsonSerializer.Deserialize<Profile>(responseData);

            return profileDto;
        }

        public async Task<Definition> CreateDefinition(string organization, string project, Definition definition)
        {
            var response = await SendAsync(new CreateDefinitionRequest(organization, project, definition));
            var responseData = await response.Content.ReadAsStringAsync();
            var definitionDto = JsonSerializer.Deserialize<Definition>(responseData);

            return definitionDto;
        }

        public async Task<IEnumerable<Definition>> GetDefinition(string organization, string project, string definitionId = default)
        {
            var response = await SendAsync(new GetDefinitionRequest(organization, project, definitionId));
            var responseData = await response.Content.ReadAsStringAsync();
            var definitionDtos = JsonSerializer.Deserialize<IEnumerable<Definition>>(responseData);

            return definitionDtos;
        }

        public async Task<string> GetDefinitionYaml(string organization, string project, string definitionId = default)
        {
            var response = await SendAsync(new GetDefinitionYamlRequest(organization, project, definitionId));
            var responseData = await response.Content.ReadAsStringAsync();

            return responseData;
        }
    }
}
