using AzureDevOpsJanitor.Domain.ValueObjects;
using ResourceProvisioning.Abstractions.Services;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Domain.Services
{
    public interface IProfileService : IService
    {
        Task<UserProfile> GetAsync(string profileIdentifier);
    }
}
