using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace ResourceProvisioning.Broker.Application
{
	public sealed class ApplicationFacadeOptions
	{
		[Required]
		public IConfigurationSection ConnectionStrings { get; set; }

		public bool EnableAutoMigrations { get; set; } = false;
	}
}
