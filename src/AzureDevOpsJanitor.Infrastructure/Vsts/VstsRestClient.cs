using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build;
using AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build.Definition;
using AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Profile;
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

        public async Task<DefinitionReference> GetDefinition(string organization, string project, int definitionId)
        {
            var response = await SendAsync(new GetDefinitionRequest(organization, project, definitionId));
            var responseData = await response.Content.ReadAsStringAsync();
            var definitionDtos = JsonSerializer.Deserialize<DefinitionReference>(responseData);

            return definitionDtos;
        }

        public async Task<string> GetDefinitionYaml(string organization, string project, int definitionId)
        {
            var response = await SendAsync(new GetDefinitionYamlRequest(organization, project, definitionId));
            var responseData = await response.Content.ReadAsStringAsync();

            return responseData;
        }

        public async Task<DefinitionReference> CreateDefinition(string organization, string project, DefinitionReference definition)
        {
            var response = await SendAsync(new CreateDefinitionRequest(organization, project, definition));
            var responseData = await response.Content.ReadAsStringAsync();
            var definitionDto = JsonSerializer.Deserialize<DefinitionReference>(responseData);

            return definitionDto;
        }

        public async Task<BuildReference> QueueBuild(string organization, string project, DefinitionReference definition)
        {
            var response = await SendAsync(new QueueBuildRequest(organization, project, definition));
            var responseData = await response.Content.ReadAsStringAsync();
            var definitionDto = JsonSerializer.Deserialize<BuildReference>(responseData);

            return definitionDto;
        }

        public async Task<BuildReference> QueueBuild(string organization, string project, int definitionId)
        {
            var response = await SendAsync(new QueueBuildRequest(organization, project, definitionId));
            var responseData = await response.Content.ReadAsStringAsync();
            var definitionDto = JsonSerializer.Deserialize<BuildReference>(responseData);

            return definitionDto;
        }
    }
}
