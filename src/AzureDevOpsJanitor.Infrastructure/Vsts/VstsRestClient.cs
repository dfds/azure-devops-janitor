using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build;
using AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build.Definition;
using AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Profile;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Vsts
{
    public sealed class VstsRestClient : HttpClient, IVstsRestClient
    {
        public const string VstsAccessTokenCacheKey = "vstsAccessToken";

        public VstsRestClient(string pat) : this()
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format(":{0}", pat))));
        }

        public VstsRestClient(JwtSecurityToken accessToken) : this()
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken.RawData);
        }

        public VstsRestClient() : base() {
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ProfileDto> GetProfile(string profileId)
        {
            var response = await SendAsync(new GetProfileRequest(profileId));
            var responseData = await response.Content.ReadAsStringAsync();
            var profileDto = JsonSerializer.Deserialize<ProfileDto>(responseData);

            return profileDto;
        }

        public async Task<DefinitionReferenceDto> GetDefinition(string organization, string project, int definitionId)
        {
            var response = await SendAsync(new GetDefinitionRequest(organization, project, definitionId));
            var responseData = await response.Content.ReadAsStringAsync();
            var definitionDtos = JsonSerializer.Deserialize<DefinitionReferenceDto>(responseData);

            return definitionDtos;
        }

        public async Task<string> GetDefinitionYaml(string organization, string project, int definitionId)
        {
            var response = await SendAsync(new GetDefinitionYamlRequest(organization, project, definitionId));
            var responseData = await response.Content.ReadAsStringAsync();

            return responseData;
        }

        public async Task<DefinitionReferenceDto> CreateDefinition(string organization, string project, DefinitionReferenceDto definition)
        {
            var response = await SendAsync(new CreateDefinitionRequest(organization, project, definition));
            var responseData = await response.Content.ReadAsStringAsync();
            var definitionDto = JsonSerializer.Deserialize<DefinitionReferenceDto>(responseData);

            return definitionDto;
        }

        public async Task<BuildDto> QueueBuild(string organization, string project, DefinitionReferenceDto definition)
        {
            var response = await SendAsync(new QueueBuildRequest(organization, project, definition));
            var responseData = await response.Content.ReadAsStringAsync();
            var definitionDto = JsonSerializer.Deserialize<BuildDto>(responseData);

            return definitionDto;
        }

        public async Task<BuildDto> QueueBuild(string organization, string project, int definitionId)
        {
            var response = await SendAsync(new QueueBuildRequest(organization, project, definitionId));
            var responseData = await response.Content.ReadAsStringAsync();
            var definitionDto = JsonSerializer.Deserialize<BuildDto>(responseData);

            return definitionDto;
        }

        public async Task<IEnumerable<TeamProjectDto>> GetProjects(string organization)
        {
            var response = await SendAsync(new GetProjectRequest(organization));
            var responseData = await response.Content.ReadAsStringAsync();
            var definitionDtos = JsonSerializer.Deserialize<VstsListResult<List<TeamProjectDto>>>(responseData);

            return definitionDtos.Value;
        }

        private class VstsListResult<T>
        {
            [JsonPropertyName("value")]
            public T Value { get; set; }
        }
    }
}
