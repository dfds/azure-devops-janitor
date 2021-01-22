using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using ResourceProvisioning.Abstractions.Protocols.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Vsts
{
    public interface IVstsClient : IRestClient
    {
        Task<IEnumerable<ProjectDto>> GetProjects(string organization, CancellationToken cancellationToken = default);

        Task<OperationDto> CreateProject(string organization, ProjectDto project, CancellationToken cancellationToken = default);

        Task<OperationDto> UpdateProject(string organization, ProjectDto project, CancellationToken cancellationToken = default);

        Task<ProfileDto> GetProfile(string profileId, CancellationToken cancellationToken = default);

        Task<DefinitionDto> GetDefinition(string organization, string project, int definitionId, CancellationToken cancellationToken = default);

        Task<string> GetDefinitionYaml(string organization, string project, int definitionId, CancellationToken cancellationToken = default);

        Task<DefinitionDto> CreateDefinition(string organization, string project, DefinitionDto definition, CancellationToken cancellationToken = default);

        Task<DefinitionDto> UpdateDefinition(string organization, string project, DefinitionDto definition, CancellationToken cancellationToken = default);

        Task<BuildDto> GetBuild(string organization, string project, int buildId, CancellationToken cancellationToken = default);

        Task<IEnumerable<ChangeDto>> GetBuildChanges(string organization, string project, int fromBuildId, int toBuildId, CancellationToken cancellationToken = default);

        Task<IEnumerable<WorkItemDto>> GetBuildWorkItemRefs(string organization, string project, int buildId, CancellationToken cancellationToken = default);

        Task DeleteBuild(string organization, string project, int buildId, CancellationToken cancellationToken = default);

        Task<BuildDto> UpdateBuild(string organization, string project, BuildDto build, CancellationToken cancellationToken = default);

        Task<BuildDto> QueueBuild(string organization, string project, int definitionId, CancellationToken cancellationToken = default);

        Task<BuildDto> QueueBuild(string organization, string project, DefinitionDto definition, CancellationToken cancellationToken = default);
    }
}