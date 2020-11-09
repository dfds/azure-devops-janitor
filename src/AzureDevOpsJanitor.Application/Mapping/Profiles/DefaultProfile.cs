using AutoMapper;
using AzureDevOpsJanitor.Domain.ValueObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;

namespace AzureDevOpsJanitor.Application.Mapping.Profiles
{
	public class DefaultProfile : Profile
	{
		public DefaultProfile()
		{
			CreateMap<VstsProfile, UserProfile>();
		}
	}
}
