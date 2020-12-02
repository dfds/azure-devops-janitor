using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects.Apis;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Vsts
{
    public interface IVstsRestClient
    {
        Task<Profile> GetProfile(string profileId);

        Task<DefinitionReference> GetDefinition(string organization, string project, int definitionId);

        Task<string> GetDefinitionYaml(string organization, string project, int definitionId);

        Task<DefinitionReference> CreateDefinition(string organization, string project, DefinitionReference definition);

        Task<BuildReference> QueueBuild(string organization, string project, int definitionId);

        Task<BuildReference> QueueBuild(string organization, string project, DefinitionReference definition);
    }
}