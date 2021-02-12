using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        private const string CapabilityIdentifierRegEx = @"\w{0,22}-\w{5}";

        public IAggregateRoot Convert(VstsDto source, IAggregateRoot destination, ResolutionContext context)
        {
            switch (source)
            {
                case BuildDto build:
                    var capabilityIdentifier = MatchCapabilityIdentifier(build.Tags);
                    var buildDef = new BuildDefinition(build.Definition.Name, string.Empty, build.Definition.Id);
                    var buildRoot = new BuildRoot(build.Project.Id, capabilityIdentifier, buildDef, build.Tags?.Select(o => new Tag(o)));

                    return buildRoot;

                case ProjectDto project:
                    var projectRoot = new ProjectRoot(project.Name);

                    return projectRoot;

                case ReleaseDto release:
                    var releaseRoot = new ReleaseRoot(release.Name);

                    return releaseRoot;

                default:
                    return null;
            }
        }

        private static string MatchCapabilityIdentifier(IEnumerable<string> buildTags)
        {
            foreach (var tag in buildTags)
            {
                if (Regex.IsMatch(tag, CapabilityIdentifierRegEx))
                {
                    return tag;
                }
            }

            throw new ArgumentException("Unable to match CapabilityIdentifier");
        }
    }
}
