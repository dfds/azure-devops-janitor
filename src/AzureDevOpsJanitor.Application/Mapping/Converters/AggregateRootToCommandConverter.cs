using AutoMapper;
using AzureDevOpsJanitor.Application.Commands.Build;
using AzureDevOpsJanitor.Application.Commands.Project;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Project;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Commands;
using System;

namespace AzureDevOpsJanitor.Application.Mapping.Converters
{
    public class AggregateRootToCommandConverter : ITypeConverter<IAggregateRoot, ICommand<IAggregateRoot>>
    {
        public ICommand<IAggregateRoot> Convert(IAggregateRoot source, ICommand<IAggregateRoot> destination = default, ResolutionContext context = default)
        {
            switch (source)
            {
                case BuildRoot build:
                    if (build.Id == 0)
                    {
                        return new CreateBuildCommand(build.ProjectId, build.CapabilityIdentifier, build.Definition);
                    }
                    else
                    {
                        return new UpdateBuildCommand(build);
                    }
                case ProjectRoot project:
                    if (project.Id == Guid.Empty)
                    {
                        return new CreateProjectCommand(project.Name);
                    }
                    else
                    {
                        return new UpdateProjectCommand(project);
                    }

                case null:
                default:
                    break;
            }

            return null;
        }
    }
}
