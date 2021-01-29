using AutoMapper;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.ValueObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using ResourceProvisioning.Abstractions.Aggregates;
using System;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Mapping.Converters
{
    public class VstsDtoToAggregateRootConverter : ITypeConverter<VstsDto, IAggregateRoot>
    {
        public IAggregateRoot Convert(VstsDto source, IAggregateRoot destination, ResolutionContext context)
        {
            switch (source)
            {
                case BuildDto build:
                    var buildDef = new BuildDefinition(build.Definition.Name, string.Empty, build.Definition.Id);
                    var buildRoot = new BuildRoot(build.Project.Id, "TBD:THIS_SHOULD_BE_INFERED_VIA_TAGS_BUT_EVENTS_DOESNT_SEEM_TO_INCLUDE_THEM_IN_V1", buildDef);

                    return buildRoot;

                case ChangeDto change:
                    throw new NotImplementedException();

                case DefinitionDto definition:
                    throw new NotImplementedException();

                case OperationDto operation:
                    throw new NotImplementedException();

                case ProfileDto profile:
                    throw new NotImplementedException();

                case ProjectDto project:
                    throw new NotImplementedException();

                case WorkItemDto workItem:
                    throw new NotImplementedException();

                default:
                    return null;
            }
        }
    }
}
