using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Vsts
{
    public interface IVstsRestClient
    {
        Task<Profile> GetProfile(string profileId);

        Task<DefinitionReference> CreateDefinition(string organization, string project, DefinitionReference definition);
    }
}