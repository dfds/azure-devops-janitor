using AzureDevOpsJanitor.Domain.ValueObjects;
using ResourceProvisioning.Abstractions.Commands;
using System.Runtime.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Profile
{
	[DataContract]
	public sealed class GetProfileCommand : ICommand<UserProfile>
	{
		public string ProfileIdentifier { get; }

		public GetProfileCommand(string profileIdentifier = null)
		{
			ProfileIdentifier = profileIdentifier ?? "me";
		}
	}
}
