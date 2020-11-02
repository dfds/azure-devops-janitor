using AzureDevOpsJanitor.Application.Protocols.Http;
using AzureDevOpsJanitor.Domain;
using AzureDevOpsJanitor.Domain.Services;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
	public sealed class DeleteBuildCommandHandler : CommandHandler<DeleteBuildCommand, IProvisioningResponse>
	{
		private readonly IControlPlaneService _controlPlaneService;

		public DeleteBuildCommandHandler(IMediator mediator, IControlPlaneService controlPlaneService) : base(mediator)
		{
			_controlPlaneService = controlPlaneService ?? throw new ArgumentNullException(nameof(controlPlaneService));
		}

		public override async Task<IProvisioningResponse> Handle(DeleteBuildCommand command, CancellationToken cancellationToken = default)
		{
			if (command.BuildId > 0)
			{
				try
				{
					await _controlPlaneService.DeleteBuildAsync(command.BuildId, cancellationToken);
				}
				catch(AzureDevOpsJanitorDomainException exp)
				{
					return new ApplicationFacadeResponse(exp);
				}
				
			}
			
			return new ApplicationFacadeResponse(true);
		}
	}
}
