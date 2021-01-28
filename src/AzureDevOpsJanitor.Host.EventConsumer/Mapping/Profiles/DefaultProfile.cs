using AzureDevOpsJanitor.Host.EventConsumer.Mapping.Converters;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using ResourceProvisioning.Abstractions.Aggregates;

namespace AzureDevOpsJanitor.Host.EventConsumer.Mapping.Profiles
{
    public sealed class DefaultProfile : AutoMapper.Profile
    {
        public DefaultProfile()
        {
            CreateMap<VstsDto, IAggregateRoot>()
            .ConvertUsing<VstsDtoToAggregateRootConverter>();
        }
    }
}
