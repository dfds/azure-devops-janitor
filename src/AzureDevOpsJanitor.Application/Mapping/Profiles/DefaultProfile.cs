using AzureDevOpsJanitor.Domain.ValueObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;

namespace AzureDevOpsJanitor.Application.Mapping.Profiles
{
	public sealed class DefaultProfile : AutoMapper.Profile
	{
		public DefaultProfile()
		{
			CreateMap<ProfileDto, UserProfile>();
		}
	}
}
