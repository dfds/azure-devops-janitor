using AzureDevOpsJanitor.Domain.Services;
using AzureDevOpsJanitor.Domain.ValueObjects;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Profile
{
	public sealed class GetProfileCommandHandler : CommandHandler<GetProfileCommand, UserProfile>
	{
		private readonly IProfileService _profileService;

		public GetProfileCommandHandler(IMediator mediator, IProfileService profileService) : base(mediator)
		{
			_profileService = profileService;
		}

		public override async Task<UserProfile> Handle(GetProfileCommand command, CancellationToken cancellationToken = default)
		{
			return await _profileService.GetAsync(command.ProfileIdentifier);
		}
	}
}
