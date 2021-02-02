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

            var response = await SendAsync(request, cancellationToken);
            var profileDto = await response.Content.ReadFromJsonAsync<ProfileDto>(null, cancellationToken);

            return profileDto;
        }

        public async Task<BuildDefinitionDto> UpdateDefinition(string project, BuildDefinitionDto definition, string organization = default, CancellationToken cancellationToken = default)
        {
            var request = new UpdateBuildDefinitionRequest(organization ?? _options.Value.DefaultOrganization, project, definition);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var definitionDto = await response.Content.ReadFromJsonAsync<BuildDefinitionDto>(null, cancellationToken);

            return definitionDto;
        }

        public async Task<BuildDefinitionDto> GetDefinition(string project, int definitionId, string organization = default, CancellationToken cancellationToken = default)
        {
            var request = new GetBuildDefinitionRequest(organization ?? _options.Value.DefaultOrganization, project, definitionId);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var definitionDto = await response.Content.ReadFromJsonAsync<BuildDefinitionDto>(null, cancellationToken);

            return definitionDto;
        }

        public async Task<string> GetDefinitionYaml(string project, int definitionId, string organization = default, CancellationToken cancellationToken = default)
        {
            var request = new GetBuildDefinitionYamlRequest(organization ?? _options.Value.DefaultOrganization, project, definitionId);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var responseData = await response.Content.ReadAsStringAsync(cancellationToken);

            return responseData;
        }

        public async Task<BuildDefinitionDto> CreateDefinition(string project, BuildDefinitionDto definition, string organization = default, CancellationToken cancellationToken = default)
        {
            var request = new UpdateBuildDefinitionRequest(organization ?? _options.Value.DefaultOrganization, project, definition);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var definitionDto = await response.Content.ReadFromJsonAsync<BuildDefinitionDto>(null, cancellationToken);

            return definitionDto;
        }

        public async Task<BuildDto> QueueBuild(string project, BuildDefinitionDto definition, string organization = default, CancellationToken cancellationToken = default)
        {
            var request = new UpdateBuildDefinitionRequest(organization ?? _options.Value.DefaultOrganization, project, definition);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var buildDto = await response.Content.ReadFromJsonAsync<BuildDto>(null, cancellationToken);

            return buildDto;
        }

        public async Task<BuildDto> QueueBuild(string project, int definitionId, string organization = default, CancellationToken cancellationToken = default)
        {
            var request = new QueueBuildRequest(organization ?? _options.Value.DefaultOrganization, project, definitionId);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var buildDto = await response.Content.ReadFromJsonAsync<BuildDto>(null, cancellationToken);

            return buildDto;
        }

        public async Task<OperationDto> CreateProject(ProjectDto project, string organization = default, CancellationToken cancellationToken = default)
        {
            var request = new CreateProjectRequest(organization ?? _options.Value.DefaultOrganization, project);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var operationReferenceDto = await response.Content.ReadFromJsonAsync<OperationDto>(null, cancellationToken);

            return operationReferenceDto;
        }

        public async Task<OperationDto> UpdateProject(ProjectDto project, string organization = default, CancellationToken cancellationToken = default)
        {
            var request = new UpdateProjectRequest(organization ?? _options.Value.DefaultOrganization, project);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var operationReferenceDto = await response.Content.ReadFromJsonAsync<OperationDto>(null, cancellationToken);

            return operationReferenceDto;
        }

        public async Task<ProjectDto> GetProject(string projectIdentifier, string organization = default, CancellationToken cancellationToken = default)
        {
            var request = new GetProjectRequest(organization ?? _options.Value.DefaultOrganization, projectIdentifier);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var projectDto = await response.Content.ReadFromJsonAsync<ProjectDto>(null, cancellationToken);

            return projectDto;
        }

        public async Task<IEnumerable<ProjectDto>> GetProjects(string organization = default, CancellationToken cancellationToken = default)
        {
            var request = new GetProjectsRequest(organization ?? _options.Value.DefaultOrganization);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var projectDtos = await response.Content.ReadFromJsonAsync<VstsListResult<List<ProjectDto>>>(null, cancellationToken);

            return projectDtos.Value;
        }

        public async Task<BuildDto> GetBuild(string project, int buildId, string organization = default, CancellationToken cancellationToken = default)
        {
            var request = new GetBuildRequest(organization ?? _options.Value.DefaultOrganization, project, buildId);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var buildDto = await response.Content.ReadFromJsonAsync<BuildDto>(null, cancellationToken);

            return buildDto;
        }

        public async Task DeleteBuild(string project, int buildId, string organization = default, CancellationToken cancellationToken = default)
        {
            var request = new GetBuildRequest(organization ?? _options.Value.DefaultOrganization, project, buildId);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new VstsClientException("Could not find requested buildId");
            }

            return;
        }

        public async Task<BuildDto> UpdateBuild(string project, BuildDto build, string organization = default, CancellationToken cancellationToken = default)
        {
            var request = new UpdateBuildRequest(organization ?? _options.Value.DefaultOrganization, project, build);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var buildDto = await response.Content.ReadFromJsonAsync<BuildDto>(null, cancellationToken);

            return buildDto;
        }

        public async Task<IEnumerable<ChangeDto>> GetBuildChanges(string project, int fromBuildId, int toBuildId, string organization = default, CancellationToken cancellationToken = default)
        {
            var request = new GetChangesBetweenBuilds(organization ?? _options.Value.DefaultOrganization, project, fromBuildId, toBuildId);

            request.Headers.Authorization = GetAuthZHeader();

            var response = await SendAsync(request, cancellationToken);
            var changeDtos = await response.Content.ReadFromJsonAsync<VstsListResult<List<ChangeDto>>>(null, cancellationToken);

            return changeDtos.Value;
        }

        public async Task<IEnumerable<WorkItemDto>> GetBuildWorkItemRefs(string project, int buildId, string organization = default, CancellationToken cancellationToken = default)
        {
            var request = new GetBuildWorkItemRefs(organization ?? _options.Value.DefaultOrganization, project, buildId);

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
