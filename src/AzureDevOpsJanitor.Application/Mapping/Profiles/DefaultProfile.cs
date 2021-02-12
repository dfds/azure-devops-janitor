using AzureDevOpsJanitor.Application.Mapping.Converters;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Commands;

namespace AzureDevOpsJanitor.Application.Mapping.Profiles
{
    public sealed class DefaultProfile : AutoMapper.Profile
    {
        public DefaultProfile()
        {
            CreateMap<IAggregateRoot, ICommand<IAggregateRoot>>()
            .ConvertUsing<AggregateRootToCommandConverter>();
        }
    }
}
