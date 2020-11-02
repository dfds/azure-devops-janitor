using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ResourceProvisioning.Abstractions.Grid;
using ResourceProvisioning.Abstractions.Grid.Provisioning;

namespace ResourceProvisioning.Broker.Application
{
	public sealed class ApplicationFacade : IProvisioningBroker
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly ILogger<ApplicationFacade> _logger;

		public Guid Id => Guid.NewGuid();

		public GridActorType ActorType => GridActorType.System;

		public ApplicationFacade(IMediator mediator, IMapper mapper, ILogger<ApplicationFacade> logger)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<IProvisioningResponse> Handle(IProvisioningRequest request, CancellationToken cancellationToken = default)
		{
			var response = await _mediator.Send(request, cancellationToken);
			var provisioningEvent = _mapper.Map<IProvisioningResponse, IProvisioningEvent>(response);

			await _mediator.Publish(provisioningEvent, cancellationToken);

			return response;
		}

		public Task Handle(IProvisioningEvent @event, CancellationToken cancellationToken = default)
		{
			_logger.LogInformation("Simple event handler logic!", @event);

			return Task.CompletedTask;
		}
	}
}
