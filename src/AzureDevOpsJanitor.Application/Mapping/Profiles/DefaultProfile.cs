using AzureDevOpsJanitor.Application.Mapping.Converters;
using AzureDevOpsJanitor.Domain.ValueObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Commands;

namespace AzureDevOpsJanitor.Application.Mapping.Profiles
{
    public sealed class DefaultProfile : AutoMapper.Profile
    {
        public DefaultProfile()
        {
            //TODO: Finalize and test profile (Aggregate -> Dto mapping, etc)
            CreateMap<ProfileDto, UserProfile>()
            .ReverseMap();

            CreateMap<DefinitionDto, BuildDefinition>()
            .ReverseMap();

            CreateMap<IAggregateRoot, ICommand<IAggregateRoot>>()
            .ConvertUsing<AggregateRootToCommandConverter>();
        }
    }
}
