using Confluent.Kafka;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Kafka.Serialization
{
    class DefaultValueSerializer<T> : IAsyncSerializer<T>
    {
        public Task<byte[]> SerializeAsync(T data, SerializationContext context)
        {
            return Task.FromResult(JsonSerializer.SerializeToUtf8Bytes(data, typeof(T)));
        }
    }
}
