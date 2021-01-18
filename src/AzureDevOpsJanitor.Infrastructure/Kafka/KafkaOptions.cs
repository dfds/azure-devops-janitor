using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureDevOpsJanitor.Infrastructure.Kafka
{
    public class KafkaOptions
    {
        [Required]
        public IDictionary<string, string> Configuration { get; set; }

        [Required]
        public IEnumerable<string> Topics { get; set; }

        public bool EnablePartitionEof { get; set; } = false;

        public int StatisticsIntervalMs { get; set; } = 5000;

        public int CommitPeriod { get; set; } = 5;
    }
}
