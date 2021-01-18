using AzureDevOpsJanitor.Infrastructure.Kafka;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace AzureDevOpsJanitor.Application
{
	public sealed class ApplicationFacadeOptions
	{
		[Required]
		public IConfigurationSection ConnectionStrings { get; set; }

		public KafkaOptions KafkaOptions { get; set; }

		public bool EnableAutoMigrations { get; set; } = false;
	}
}
