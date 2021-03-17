using AutoMapper;
using AzureDevOpsJanitor.Domain.Aggregates.Project;
using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Infrastructure.Azure.DevOps.DataTransferObjects.Project;

namespace AzureDevOpsJanitor.Application.Mapping.Converters
{
    public class ProjectRootToProjectDtoConverter : ITypeConverter<ProjectRoot, ProjectDto>, ITypeConverter<ProjectDto, IAggregateRoot>
    {
        public IAggregateRoot Convert(ProjectDto source, IAggregateRoot destination = default, ResolutionContext context = default)
        {
            var projectRoot = new ProjectRoot(source.Name);

            return projectRoot;
        }

        public ProjectDto Convert(ProjectRoot source, ProjectDto destination, ResolutionContext context)
        {
            var projectDto = new ProjectDto
            {
                Name = source.Name
            };

            return projectDto;
        }
    }
}
