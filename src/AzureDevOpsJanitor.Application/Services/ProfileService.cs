using AutoMapper;
using AzureDevOpsJanitor.Domain.Services;
using AzureDevOpsJanitor.Domain.ValueObjects;
using CloudEngineering.CodeOps.Infrastructure.AzureDevOps;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Services
{
    public sealed class ProfileService : IProfileService
    {
        private readonly IMapper _mapper;
        private readonly IAdoClient _client;

        public ProfileService(IMapper mapper, IAdoClient client)
        {
            _mapper = mapper;
            _client = client;
        }

        public async Task<UserProfile> GetAsync(string profileIdentifier)
        {
            var profileData = await _client.GetProfile(profileIdentifier);

            return _mapper.Map<UserProfile>(profileData);
        }
    }
}
