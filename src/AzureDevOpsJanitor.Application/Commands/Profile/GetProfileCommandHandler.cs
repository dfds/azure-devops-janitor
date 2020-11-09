using AzureDevOpsJanitor.Domain.Services;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Profile
{
	public sealed class GetProfileCommandHandler : CommandHandler<GetProfileCommand, Domain.ValueObjects.Profile>
	{
		private readonly IProfileService _profileService;

		public GetProfileCommandHandler(IMediator mediator, IProfileService profileService) : base(mediator)
		{
			_profileService = profileService;
		}

		public override async Task<Domain.ValueObjects.Profile> Handle(GetProfileCommand command, CancellationToken cancellationToken = default)
		{
			return await _profileService.GetProfileAsync();
		}
	}
}
