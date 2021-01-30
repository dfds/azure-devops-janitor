using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build;
using AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build.Definition;
using AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Profile;
using AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Project;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Abstractions.Protocols.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Vsts
{
    public sealed class VstsClient : RestClient, IVstsClient
    {
        private readonly IOptions<VstsClientOptions> _options;

        public VstsClient(IOptions<VstsClientOptions> options) : base(new SocketsHttpHandler())
        {
            _options = options;
        }

        private AuthenticationHeaderValue GetAuthZHeader()
        {
            if (Regex.IsMatch(_options.Value.ClientSecret, JwtConstants.JsonCompactSerializationRegex) || Regex.IsMatch(_options.Value.ClientSecret, JwtConstants.JweCompactSerializationRegex))
            {
                return new AuthenticationHeaderValue("Bearer", _options.Value.ClientSecret);
            }
            else
            {
                return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format(":{0}", _options.Value.ClientSecret))));
            }
        }

        public async Task<ProfileDto> GetProfile(string profileId, CancellationToken cancellationToken = default)
        {
            var request = new GetProfileRequest(profileId);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(new GetProfileRequest(profileId), cancellationToken);
            var profileDto = await response.Content.ReadFromJsonAsync<ProfileDto>(null, cancellationToken);

            return profileDto;
        }

        public async Task<BuildDefinitionDto> UpdateDefinition(string organization, string project, BuildDefinitionDto definition, CancellationToken cancellationToken = default)
        {
            var request = new UpdateBuildDefinitionRequest(organization, project, definition);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var definitionDto = await response.Content.ReadFromJsonAsync<BuildDefinitionDto>(null, cancellationToken);

            return definitionDto;
        }

        public async Task<BuildDefinitionDto> GetDefinition(string organization, string project, int definitionId, CancellationToken cancellationToken = default)
        {
            var request = new GetBuildDefinitionRequest(organization, project, definitionId);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(new GetBuildDefinitionRequest(organization, project, definitionId), cancellationToken);
            var definitionDto = await response.Content.ReadFromJsonAsync<BuildDefinitionDto>(null, cancellationToken);

            return definitionDto;
        }

        public async Task<string> GetDefinitionYaml(string organization, string project, int definitionId, CancellationToken cancellationToken = default)
        {
            var request = new GetBuildDefinitionYamlRequest(organization, project, definitionId);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(new GetBuildDefinitionYamlRequest(organization, project, definitionId), cancellationToken);
            var responseData = await response.Content.ReadAsStringAsync(cancellationToken);

            return responseData;
        }

        public async Task<BuildDefinitionDto> CreateDefinition(string organization, string project, BuildDefinitionDto definition, CancellationToken cancellationToken = default)
        {
            var request = new UpdateBuildDefinitionRequest(organization, project, definition);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(new CreateBuildDefinitionRequest(organization, project, definition), cancellationToken);
            var definitionDto = await response.Content.ReadFromJsonAsync<BuildDefinitionDto>(null, cancellationToken);

            return definitionDto;
        }

        public async Task<BuildDto> QueueBuild(string organization, string project, BuildDefinitionDto definition, CancellationToken cancellationToken = default)
        {
            var request = new UpdateBuildDefinitionRequest(organization, project, definition);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(new QueueBuildRequest(organization, project, definition), cancellationToken);
            var buildDto = await response.Content.ReadFromJsonAsync<BuildDto>(null, cancellationToken);

            return buildDto;
        }

        public async Task<BuildDto> QueueBuild(string organization, string project, int definitionId, CancellationToken cancellationToken = default)
        {
            var request = new QueueBuildRequest(organization, project, definitionId);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var buildDto = await response.Content.ReadFromJsonAsync<BuildDto>(null, cancellationToken);

            return buildDto;
        }

        public async Task<OperationDto> CreateProject(string organization, ProjectDto project, CancellationToken cancellationToken = default)
        {
            var request = new CreateProjectRequest(organization, project);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var operationReferenceDto = await response.Content.ReadFromJsonAsync<OperationDto>(null, cancellationToken);

            return operationReferenceDto;
        }

        public async Task<OperationDto> UpdateProject(string organization, ProjectDto project, CancellationToken cancellationToken = default)
        {
            var request = new UpdateProjectRequest(organization, project);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var operationReferenceDto = await response.Content.ReadFromJsonAsync<OperationDto>(null, cancellationToken);

            return operationReferenceDto;
        }

        public async Task<IEnumerable<ProjectDto>> GetProjects(string organization, CancellationToken cancellationToken = default)
        {
            var request = new GetProjectRequest(organization);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var definitionDtos = await response.Content.ReadFromJsonAsync<VstsListResult<List<ProjectDto>>>(null, cancellationToken);

            return definitionDtos.Value;
        }

        public async Task<BuildDto> GetBuild(string organization, string project, int buildId, CancellationToken cancellationToken = default)
        {
            var request = new GetBuildRequest(organization, project, buildId);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var buildDto = await response.Content.ReadFromJsonAsync<BuildDto>(null, cancellationToken);

            return buildDto;
        }

        public async Task DeleteBuild(string organization, string project, int buildId, CancellationToken cancellationToken = default)
        {
            var request = new GetBuildRequest(organization, project, buildId);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new VstsClientException("Could not find requested buildId");
            }

            return;
        }

        public async Task<BuildDto> UpdateBuild(string organization, string project, BuildDto build, CancellationToken cancellationToken = default)
        {
            var request = new UpdateBuildRequest(organization, project, build);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var buildDto = await response.Content.ReadFromJsonAsync<BuildDto>(null, cancellationToken);

            return buildDto;
        }

        public async Task<IEnumerable<ChangeDto>> GetBuildChanges(string organization, string project, int fromBuildId, int toBuildId, CancellationToken cancellationToken = default)
        {
            var request = new GetChangesBetweenBuilds(organization, project, fromBuildId, toBuildId);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var changeDtos = await response.Content.ReadFromJsonAsync<VstsListResult<List<ChangeDto>>>(null, cancellationToken);

            return changeDtos.Value;
        }

        public async Task<IEnumerable<WorkItemDto>> GetBuildWorkItemRefs(string organization, string project, int buildId, CancellationToken cancellationToken = default)
        {
            var request = new GetBuildWorkItemRefs(organization, project, buildId);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var workItemDtos = await response.Content.ReadFromJsonAsync<VstsListResult<List<WorkItemDto>>>(null, cancellationToken);

            return workItemDtos.Value;
        }

        private class VstsListResult<T>
        {
            [JsonPropertyName("value")]
            public T Value { get; set; }
        }
    }
}
