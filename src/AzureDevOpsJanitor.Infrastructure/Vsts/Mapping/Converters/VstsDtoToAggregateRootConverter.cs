using AutoMapper;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Aggregates.Release;
using AzureDevOpsJanitor.Domain.ValueObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using ResourceProvisioning.Abstractions.Aggregates;

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

                case ProjectDto project:
                    var projectRoot = new ProjectRoot(project.Name);

                    return projectRoot;

                case ReleaseDto release:
                    //TODO: Finish mapping this object once VSTS Release APIs are integrated
                    var releaseRoot = new ReleaseRoot(release.Name);

                    return releaseRoot;

                default:
                    return null;
            }
        }
    }
}
