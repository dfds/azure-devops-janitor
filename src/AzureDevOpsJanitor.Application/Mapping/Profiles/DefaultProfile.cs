using AzureDevOpsJanitor.Application.Mapping.Converters;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Aggregates.Release;
using AzureDevOpsJanitor.Domain.ValueObjects;
using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Commands;
using CloudEngineering.CodeOps.Infrastructure.Azure.DevOps.DataTransferObjects.Build;
using CloudEngineering.CodeOps.Infrastructure.Azure.DevOps.DataTransferObjects.Profile;
using CloudEngineering.CodeOps.Infrastructure.Azure.DevOps.DataTransferObjects.Project;
using CloudEngineering.CodeOps.Infrastructure.Azure.DevOps.DataTransferObjects.Release;
using CloudEngineering.CodeOps.Infrastructure.Azure.DevOps.DataTransferObjects.Shared;

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
