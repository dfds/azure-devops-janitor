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
	public sealed class GetBuildCommandHandler : CommandHandler<GetBuildCommand, IProvisioningResponse>
	{
		private readonly IControlPlaneService _controlPlaneService;

		public GetBuildCommandHandler(IMediator mediator, IControlPlaneService controlPlaneService) : base(mediator)
		{
			_controlPlaneService = controlPlaneService ?? throw new ArgumentNullException(nameof(controlPlaneService));
		}

		public override async Task<IProvisioningResponse> Handle(GetBuildCommand command, CancellationToken cancellationToken = default)
		{
			var result = await _controlPlaneService.GetBuildByIdAsync(command.BuildId);
			
			return new ApplicationFacadeResponse(result);
		}
	}
}
