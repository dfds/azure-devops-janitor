using AzureDevOpsJanitor.Application.Mapping.Converters;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Aggregates.Release;
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
            CreateMap<IAggregateRoot, ICommand<IAggregateRoot>>()
            .ConvertUsing<AggregateRootToCommandConverter>();

            CreateMap<BuildRoot, BuildDto>()
            .ConvertUsing<BuildRootToBuildDtoConverter>();
            
            CreateMap<BuildDto, IAggregateRoot>()
            .ConvertUsing<BuildRootToBuildDtoConverter>();

            CreateMap<ReleaseRoot, ReleaseDto>()
            .ConvertUsing<ReleaseRootToReleaseDtoConverter>();

            CreateMap<ReleaseDto, IAggregateRoot>()
            .ConvertUsing<ReleaseRootToReleaseDtoConverter>();

            CreateMap<ProjectRoot, ProjectDto>()
            .ConvertUsing<ProjectRootToProjectDtoConverter>();

            CreateMap<ProjectDto, IAggregateRoot>()
            .ConvertUsing<ProjectRootToProjectDtoConverter>();

            CreateMap<Artifact, ArtifactDto>()
            .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();

            CreateMap<UserProfile, ProfileDto>()
            .ReverseMap();

            CreateMap<BuildDefinition, BuildDefinitionDto>()
            .ReverseMap();
        }
    }
}
