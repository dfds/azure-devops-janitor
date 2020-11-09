using AzureDevOpsJanitor.Domain.ValueObjects;
using ResourceProvisioning.Abstractions.Commands;
using System.Runtime.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Profile
{
	[DataContract]
	public sealed class GetProfileCommand : ICommand<UserProfile>
	{
		public string ProfileId { get; }

		public GetProfileCommand(string profileId = null)
		{			
			ProfileId = profileId ?? "me";
		}
	}
}
