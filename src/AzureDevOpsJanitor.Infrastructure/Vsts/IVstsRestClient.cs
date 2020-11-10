using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Vsts
{
    public interface IVstsRestClient
    {
        Task<Profile> GetProfile(string profileId);
    }
}