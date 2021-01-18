using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework
{
    public class DomainContextOptions
    {
        [Required]
        public IConfigurationSection ConnectionStrings { get; set; }

        public bool EnableAutoMigrations { get; set; } = false;
    }
}
