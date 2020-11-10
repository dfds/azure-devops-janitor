using AzureDevOpsJanitor.Domain.ValueObjects;
using Profile = AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects.Profile;

namespace AzureDevOpsJanitor.Application.Mapping.Profiles
{
	public class DefaultProfile : AutoMapper.Profile
	{
		public DefaultProfile()
		{
			CreateMap<Profile, UserProfile>();
		}
	}
}
