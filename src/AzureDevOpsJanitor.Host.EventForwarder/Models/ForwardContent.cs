namespace AzureDevOpsJanitor.Host.EventForwarder.Models
{
    public class ForwardContent
    {
        public string Topic { get; set; }
        public string Payload { get; set; }
    }
}