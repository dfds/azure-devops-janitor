using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureDevOpsJanitor.Infrastructure.Kafka
{
    public class KafkaOptions
    {
        [Required]
        public IDictionary<string, string> Configuration { get; set; }

        public IEnumerable<string> Topics { get; set; }
    }
}
