using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ResourceProvisioning.Broker.Host.Api.Infrastructure.Authentication.Discovery
{
	internal class Metadata
	{
		/// <summary>
		/// Preferred alias
		/// </summary>
		[JsonPropertyName("preferred_network")]
		public string PreferredNetwork { get; set; }

		/// <summary>
		/// Preferred alias to cache tokens emitted by one of the aliases (to avoid
		/// SSO islands)
		/// </summary>
		[JsonPropertyName("preferred_cache")]
		public string PreferredCache { get; set; }

		/// <summary>
		/// Aliases of issuer URLs which are equivalent
		/// </summary>
		[JsonPropertyName("aliases")]
		public List<string> Aliases { get; set; }
	}
}
