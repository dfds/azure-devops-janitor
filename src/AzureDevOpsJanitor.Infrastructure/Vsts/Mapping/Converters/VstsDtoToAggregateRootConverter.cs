using AutoMapper;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.ValueObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using ResourceProvisioning.Abstractions.Aggregates;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Mapping.Converters
{
    public class VstsDtoToAggregateRootConverter : ITypeConverter<VstsDto, IAggregateRoot>
    {
        public IAggregateRoot Convert(VstsDto source, IAggregateRoot destination, ResolutionContext context)
        {
            //TODO: Implement support for all required VstsDtos
            switch (source)
            {
                case BuildDto build:
                    var buildDef = new BuildDefinition(build.Definition.Name, string.Empty, build.Definition.Id);
                    var buildRoot = new BuildRoot(build.Project.Id, "TBD:THIS_SHOULD_BE_INFERED_VIA_TAGS_BUT_EVENTS_DOESNT_SEEM_TO_INCLUDE_THEM_IN_V1", buildDef);

                    return buildRoot;

                default:

                    return null;
            }
        }
    }
}
