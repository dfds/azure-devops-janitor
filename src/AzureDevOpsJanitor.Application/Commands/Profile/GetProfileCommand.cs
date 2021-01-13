using AzureDevOpsJanitor.Domain.ValueObjects;
using ResourceProvisioning.Abstractions.Commands;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Profile
{
	public sealed class GetProfileCommand : ICommand<UserProfile>
	{
		[JsonPropertyName("profileIdentifier")]
		public string ProfileIdentifier { get; private set; }

		[JsonConstructor]
		public GetProfileCommand(string profileIdentifier = null)
		{
			ProfileIdentifier = profileIdentifier ?? "me";
		}
	}
}
