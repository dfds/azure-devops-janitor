using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Vsts
{
    public interface IVstsRestClient
    {
        Task<TeamProjectDto> GetProject(string organization);

        Task<ProfileDto> GetProfile(string profileId);

        Task<DefinitionReferenceDto> GetDefinition(string organization, string project, int definitionId);

        Task<string> GetDefinitionYaml(string organization, string project, int definitionId);

        Task<DefinitionReferenceDto> CreateDefinition(string organization, string project, DefinitionReferenceDto definition);

        Task<BuildDto> QueueBuild(string organization, string project, int definitionId);

        Task<BuildDto> QueueBuild(string organization, string project, DefinitionReferenceDto definition);
    }
}