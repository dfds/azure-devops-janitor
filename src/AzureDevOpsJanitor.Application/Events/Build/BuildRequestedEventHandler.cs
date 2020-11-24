using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using AzureDevOpsJanitor.Domain.Events.Build;
using AzureDevOpsJanitor.Infrastructure.Vsts;
using AutoMapper;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;

namespace AzureDevOpsJanitor.Application.Events.Build
{
	public sealed class BuildRequestedEventHandler : IEventHandler<BuildRequestedEvent>
	{
		private readonly IMapper _mapper;
		private readonly IVstsRestClient _restClient;

		public BuildRequestedEventHandler(IMapper mapper, IVstsRestClient restClient) 
		{
			_mapper = mapper;
			_restClient = restClient;
		}

		public Task Handle(BuildRequestedEvent @event, CancellationToken cancellationToken)
		{
			//TODO: Create build via ADO REST API
			var definition = _restClient.CreateDefinition("dfds", "CloudEngineering", _mapper.Map<DefinitionReference>(@event.Build.Definition));

			//TODO: Update build state to created after API call completes. Otherwise fail it.

			return Task.CompletedTask;
		}
	}
}
