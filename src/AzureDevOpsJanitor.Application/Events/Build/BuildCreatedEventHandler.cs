using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using AzureDevOpsJanitor.Domain.Events.Build;
using AzureDevOpsJanitor.Infrastructure.Vsts;
using AutoMapper;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;

namespace AzureDevOpsJanitor.Application.Events.Build
{
	public sealed class BuildCreatedEventHandler : IEventHandler<BuildCreatedEvent>
	{
		private readonly IMapper _mapper;
		private readonly IVstsRestClient _restClient;

		public BuildCreatedEventHandler(IMapper mapper, IVstsRestClient restClient) 
		{
			_mapper = mapper;
			_restClient = restClient;
		}

		public async Task Handle(BuildCreatedEvent @event, CancellationToken cancellationToken)
		{
			await _restClient.CreateDefinition("dfds", "CloudEngineering", _mapper.Map<DefinitionReferenceDto>(@event.Build.Definition));
		}
	}
}
