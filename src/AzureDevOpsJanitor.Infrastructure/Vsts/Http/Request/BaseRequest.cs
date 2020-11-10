using System.Net.Http;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request
{
    public abstract class BaseRequest : HttpRequestMessage
    {
        public string ApiVersion { get; protected set; }
    }
}
