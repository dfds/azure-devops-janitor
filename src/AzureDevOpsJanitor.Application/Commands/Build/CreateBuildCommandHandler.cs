using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Services;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
	public sealed class CreateBuildCommandHandler : CommandHandler<CreateBuildCommand, BuildRoot>
	{
		private readonly IBuildService _buildService;

		public CreateBuildCommandHandler(IMediator mediator, IBuildService buildService) : base(mediator)
		{
			_buildService = buildService ?? throw new ArgumentNullException(nameof(buildService));
		}

		public override async Task<BuildRoot> Handle(CreateBuildCommand command, CancellationToken cancellationToken = default)
		{
			return await _buildService.AddBuildAsync(command.CapabilityId, cancellationToken);
		}
	}
}
