using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ResourceProvisioning.Broker.Host.Api.Infrastructure.Authentication.Discovery
{
	internal class IssuerMetadata
	{
		/// <summary>
		/// Tenant discovery endpoint
		/// </summary>
		[JsonPropertyName("tenant_discovery_endpoint")]
		public string TenantDiscoveryEndpoint { get; set; }

		/// <summary>
		/// API Version
		/// </summary>
		[JsonPropertyName("api-version")]
		public string ApiVersion { get; set; }

		/// <summary>
		/// List of metadata associated with the endpoint
		/// </summary>
		[JsonPropertyName("metadata")]
		public List<Metadata> Metadata { get; set; }
	}
}
