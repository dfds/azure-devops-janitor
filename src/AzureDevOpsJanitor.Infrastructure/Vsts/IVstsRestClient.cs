﻿using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using ResourceProvisioning.Abstractions.Protocols.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Infrastructure.Vsts
{
    //TODO: Class is getting bloated. We need a Facade pattern.
    public interface IVstsRestClient : IRestClient
    {
        Task<IEnumerable<TeamProjectDto>> GetProjects(string organization);

        Task<OperationReferenceDto> CreateProject(string organization, TeamProjectDto project);

        Task<OperationReferenceDto> UpdateProject(string organization, TeamProjectDto project);

        Task<ProfileDto> GetProfile(string profileId);

        Task<DefinitionReferenceDto> GetDefinition(string organization, string project, int definitionId);

        Task<string> GetDefinitionYaml(string organization, string project, int definitionId);

        Task<DefinitionReferenceDto> CreateDefinition(string organization, string project, DefinitionReferenceDto definition);

        Task<DefinitionReferenceDto> UpdateDefinition(string organization, string project, DefinitionReferenceDto definition);

        Task<BuildDto> GetBuild(string organization, string project, int buildId);

        Task<IEnumerable<ChangeDto>> GetBuildChanges(string organization, string project, int fromBuildId, int toBuildId);

        Task<IEnumerable<WorkItemDto>> GetBuildWorkItemRefs(string organization, string project, int buildId);

        Task DeleteBuild(string organization, string project, int buildId);

        Task<BuildDto> UpdateBuild(string organization, string project, BuildDto build);

        Task<BuildDto> QueueBuild(string organization, string project, int definitionId);

        Task<BuildDto> QueueBuild(string organization, string project, DefinitionReferenceDto definition);
    }
}