using System.Net.Http;
using System.Text.Json;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace AzureDevOpsJanitor.Application.Protocols.Http
{
	public sealed class ApplicationFacadeResponse : HttpResponseMessage, IProvisioningResponse
	{
		public ApplicationFacadeResponse(dynamic content = null, JsonSerializerOptions options = default)
		{
			StatusCode = System.Net.HttpStatusCode.OK;
			
			if(content != null)
			{ 
				Content = new StringContent(JsonSerializer.Serialize(content, options));
			}
		}
	}
}
