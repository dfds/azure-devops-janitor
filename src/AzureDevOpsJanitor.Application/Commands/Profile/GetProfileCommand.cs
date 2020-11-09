using ResourceProvisioning.Abstractions.Commands;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Profile
{
	[DataContract]
	public sealed class GetProfileCommand : VstsCommand, ICommand<Domain.ValueObjects.Profile>
	{
		public GetProfileCommand(JwtSecurityToken accessToken) : base(accessToken)
		{
	
		}
	}
}
