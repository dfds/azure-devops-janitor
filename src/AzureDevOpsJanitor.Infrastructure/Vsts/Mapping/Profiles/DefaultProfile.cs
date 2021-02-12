using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts.Events;
using AzureDevOpsJanitor.Infrastructure.Vsts.Mapping.Converters;
using ResourceProvisioning.Abstractions.Aggregates;
using System.Text.Json;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Mapping.Profiles
{
    public sealed class DefaultProfile : AutoMapper.Profile
    {
        public DefaultProfile()
        {
            CreateMap<JsonElement, WebHookEvent>()
            .ConvertUsing<JsonElementToWebHookEventConverter>();

            CreateMap<WebHookEvent, VstsDto>()
            .ConvertUsing<WebHookEventToVstsDtoConverter>();

            CreateMap<VstsDto, IAggregateRoot>()
            .ConvertUsing<VstsDtoToAggregateRootConverter>();
        }
    }
}
