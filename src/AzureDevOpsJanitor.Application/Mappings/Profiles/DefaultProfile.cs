using AzureDevOpsJanitor.Application.Mappings.Converters;
using AzureDevOpsJanitor.Domain.ValueObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Commands;

namespace AzureDevOpsJanitor.Application.Mappings.Profiles
{
    public sealed class DefaultProfile : AutoMapper.Profile
    {
        public DefaultProfile()
        {
            CreateMap<ProfileDto, UserProfile>()
            .ReverseMap();

            CreateMap<DefinitionDto, BuildDefinition>()
            .ReverseMap();

            CreateMap<IAggregateRoot, ICommand<IAggregateRoot>>()
            .ConvertUsing<AggregateRootToCommandConverter>();
        }
    }
}
