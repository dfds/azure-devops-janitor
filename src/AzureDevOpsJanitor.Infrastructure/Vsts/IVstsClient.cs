using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using ResourceProvisioning.Abstractions.Protocols.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Vsts
{
    public interface IVstsClient : IRestClient
    {
        Task<ProjectDto> GetProject(string projectIdentifier, string organization = default, CancellationToken cancellationToken = default);

        Task<IEnumerable<ProjectDto>> GetProjects(string organization = default, CancellationToken cancellationToken = default);

        Task<OperationDto> CreateProject(ProjectDto project, string organization = default, CancellationToken cancellationToken = default);

        Task<OperationDto> UpdateProject(ProjectDto project, string organization = default, CancellationToken cancellationToken = default);

        Task<ProfileDto> GetProfile(string profileId, CancellationToken cancellationToken = default);

        Task<BuildDefinitionDto> GetDefinition(string project, int definitionId, string organization = default, CancellationToken cancellationToken = default);

        Task<string> GetDefinitionYaml(string project, int definitionId, string organization = default, CancellationToken cancellationToken = default);

        Task<BuildDefinitionDto> CreateDefinition(string project, BuildDefinitionDto definition, string organization = default, CancellationToken cancellationToken = default);

        Task<BuildDefinitionDto> UpdateDefinition(string project, BuildDefinitionDto definition, string organization = default, CancellationToken cancellationToken = default);

        Task<BuildDto> GetBuild(string project, int buildId, string organization = default, CancellationToken cancellationToken = default);

        Task<IEnumerable<ChangeDto>> GetBuildChanges(string project, int fromBuildId, int toBuildId, string organization = default, CancellationToken cancellationToken = default);

        Task<IEnumerable<WorkItemDto>> GetBuildWorkItemRefs(string project, int buildId, string organization = default, CancellationToken cancellationToken = default);

        Task DeleteBuild(string project, int buildId, string organization = default, CancellationToken cancellationToken = default);

        Task<BuildDto> UpdateBuild(string project, BuildDto build, string organization = default, CancellationToken cancellationToken = default);

        Task<BuildDto> QueueBuild(string project, int definitionId, string organization = default, CancellationToken cancellationToken = default);

        Task<BuildDto> QueueBuild(string project, BuildDefinitionDto definition, string organization = default, CancellationToken cancellationToken = default);
    }
}