using AzureDevOpsJanitor.Domain.ValueObjects;
using CloudEngineering.CodeOps.Abstractions.Services;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Domain.Services
{
    public interface IProfileService : IService
    {
        Task<UserProfile> GetAsync(string profileIdentifier);
    }
}
