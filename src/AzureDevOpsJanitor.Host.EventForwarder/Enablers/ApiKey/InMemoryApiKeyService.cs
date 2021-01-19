using System;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.EventForwarder.Enablers.ApiKey
{
    public class InMemoryApiKeyService : IApiKeyService
    {
        public Task<bool> IsAuthorized(string clientId, string apiKey)
        {
            Console.WriteLine($"x-clientId: {clientId}");
            Console.WriteLine($"x-apiKey: {apiKey}");

            return Task.FromResult(true);
        }
    }
}