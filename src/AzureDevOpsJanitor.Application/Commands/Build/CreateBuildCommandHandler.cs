using AzureDevOpsJanitor.Application.Protocols.Http;
using AzureDevOpsJanitor.Domain.Services;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
	public sealed class CreateBuildCommandHandler : CommandHandler<CreateBuildCommand, IProvisioningResponse>
	{
		private readonly IControlPlaneService _controlPlaneService;

		public CreateBuildCommandHandler(IMediator mediator, IControlPlaneService controlPlaneService) : base(mediator)
		{
			_controlPlaneService = controlPlaneService ?? throw new ArgumentNullException(nameof(controlPlaneService));
		}

		public override async Task<IProvisioningResponse> Handle(CreateBuildCommand command, CancellationToken cancellationToken = default)
		{
			var aggregate = await _controlPlaneService.AddBuildAsync(command.CapabilityId, cancellationToken);

			return new ApplicationFacadeResponse(aggregate);
		}
	}
}
