using AutoMapper;
using AzureDevOpsJanitor.Domain.Events.Build;
using AzureDevOpsJanitor.Infrastructure.Vsts;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using ResourceProvisioning.Abstractions.Events;
using System.Threading;
using System.Threading.Tasks;

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

		public async Task Handle(BuildCreatedEvent @event, CancellationToken cancellationToken = default)
		{
			//TODO: Mapping
			await _restClient.CreateDefinition("dfds", "CloudEngineering", _mapper.Map<DefinitionReferenceDto>(@event.Build.Definition));
		}
	}
}
