using CloudEngineering.CodeOps.Abstractions.Commands;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
    public sealed class DeleteBuildCommand : ICommand<bool>
    {
        [JsonPropertyName("buildId")]
        public int BuildId { get; private set; }

        [JsonConstructor]
        public DeleteBuildCommand(int buildId)
        {
            BuildId = buildId;
        }
    }
}
