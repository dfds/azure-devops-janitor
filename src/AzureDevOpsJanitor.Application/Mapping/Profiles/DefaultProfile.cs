using AzureDevOpsJanitor.Domain.ValueObjects;
using Profile = AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects.Apis.Profile;

namespace AzureDevOpsJanitor.Application.Mapping.Profiles
{
	public sealed class DefaultProfile : AutoMapper.Profile
	{
		public DefaultProfile()
		{
			CreateMap<Profile, UserProfile>();
		}
	}
}
