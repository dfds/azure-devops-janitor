using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.EventForwarder.Enablers.ApiKey
{
    public class FileApiKeyService : IApiKeyService
    {
        private readonly KeyFile _keys;

        public FileApiKeyService()
        {
            var rawFileContent = File.ReadAllText("apikey.json");
            _keys = JsonSerializer.Deserialize<KeyFile>(rawFileContent);
        }

        public Task<bool> IsAuthorized(string clientId, string apiKey)
        {
            if (_keys.Keys.ContainsKey(apiKey))
            {
                if (_keys.Keys[apiKey] == clientId)
                {
                    return Task.FromResult(true);
                }

                return Task.FromResult(false);
            }

            return Task.FromResult(false);
        }
    }

    class KeyFile
    {
        public Dictionary<string, string> Keys { get; set; }

        public KeyFile()
        {
            Keys = new Dictionary<string, string>();
        }
    }
}