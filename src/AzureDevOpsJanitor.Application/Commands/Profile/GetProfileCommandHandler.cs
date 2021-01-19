using AzureDevOpsJanitor.Domain.Services;
using AzureDevOpsJanitor.Domain.ValueObjects;
using ResourceProvisioning.Abstractions.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Profile
{
    public sealed class GetProfileCommandHandler : ICommandHandler<GetProfileCommand, UserProfile>
    {
        private readonly IProfileService _profileService;

        public GetProfileCommandHandler(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task<UserProfile> Handle(GetProfileCommand command, CancellationToken cancellationToken = default)
        {
            return await _profileService.GetAsync(command.ProfileIdentifier);
        }
    }
}
