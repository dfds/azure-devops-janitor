using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects.Events;
using Dafda.Consuming;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.KafkaWorker.Handlers
{
    //TODO: Finish event handler
    public sealed class BuildCompletedEventHandler : IMessageHandler<BuildCompletedEvent>
    {
        public Task Handle(BuildCompletedEvent message, MessageHandlerContext context)
        {
            return Task.CompletedTask;
        }
    }
}
