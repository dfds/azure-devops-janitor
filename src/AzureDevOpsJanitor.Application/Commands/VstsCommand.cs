using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Serialization;

namespace AzureDevOpsJanitor.Application.Commands
{
	public abstract class VstsCommand
	{
		[DataMember]
		public JwtSecurityToken AccessToken { get; protected set; }

		protected VstsCommand(JwtSecurityToken accessToken)
		{
			AccessToken = accessToken;
		}
	}
}
