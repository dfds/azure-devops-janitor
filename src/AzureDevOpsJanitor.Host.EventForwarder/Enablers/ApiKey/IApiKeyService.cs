using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.EventForwarder.Enablers.ApiKey
{
    public interface IApiKeyService
    {
        Task<bool> IsAuthorized(string clientId, string apiKey);
    }
}