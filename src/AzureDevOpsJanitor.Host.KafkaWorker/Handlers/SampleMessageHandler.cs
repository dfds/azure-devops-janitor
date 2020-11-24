using Dafda.Consuming;
using System;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.KafkaWorker.Handlers
{
    public class SampleMessageHandler : IMessageHandler<SampleMessage>
    {
        public Task Handle(SampleMessage message, MessageHandlerContext context)
        {
            return Task.CompletedTask;
        }
    }
}
