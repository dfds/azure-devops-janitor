using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Vsts
{
    public interface IVstsRestClient
    {
        Task<IEnumerable<TeamProjectDto>> GetProjects(string organization);

        Task<ProfileDto> GetProfile(string profileId);

        Task<DefinitionReferenceDto> GetDefinition(string organization, string project, int definitionId);

        Task<string> GetDefinitionYaml(string organization, string project, int definitionId);

        Task<DefinitionReferenceDto> CreateDefinition(string organization, string project, DefinitionReferenceDto definition);

        Task<BuildDto> QueueBuild(string organization, string project, int definitionId);

        Task<BuildDto> QueueBuild(string organization, string project, DefinitionReferenceDto definition);
    }
}