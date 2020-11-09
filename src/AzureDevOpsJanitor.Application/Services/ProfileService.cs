using AutoMapper;
using AzureDevOpsJanitor.Domain.Services;
using AzureDevOpsJanitor.Domain.ValueObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Services
{
	public sealed class ProfileService : IProfileService
	{
		private readonly IMapper _mapper;
		private readonly IVstsRestClient _client;

		public ProfileService(IMapper mapper, IVstsRestClient client)
		{
			_mapper = mapper;
			_client = client;
		}

		public async Task<UserProfile> GetProfileAsync(string profileId)
		{
			var profileData = await _client.GetProfile(profileId);

			return _mapper.Map<UserProfile>(profileData);
		}
	}
}
