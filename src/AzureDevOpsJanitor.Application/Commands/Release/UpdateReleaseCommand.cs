using AzureDevOpsJanitor.Domain.Aggregates.Release;
using CloudEngineering.CodeOps.Abstractions.Commands;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Release
{
    public sealed class UpdateReleaseCommand : ICommand<ReleaseRoot>
    {
        [JsonPropertyName("release")]
        public ReleaseRoot Release { get; init; }

        [JsonConstructor]
        public UpdateReleaseCommand(ReleaseRoot release)
        {
            Release = release;
        }
    }
}
