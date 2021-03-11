using AutoMapper;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.ValueObjects;
using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Infrastructure.AzureDevOps.DataTransferObjects.Build;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AzureDevOpsJanitor.Application.Mapping.Converters
{
    public class BuildRootToBuildDtoConverter : ITypeConverter<BuildRoot, BuildDto>, ITypeConverter<BuildDto, IAggregateRoot>
    {
        private const string CapabilityIdentifierRegEx = @"\w{0,22}-\w{5}";

        public BuildDto Convert(BuildRoot source, BuildDto destination = default, ResolutionContext context = default)
        {
            throw new NotImplementedException();
        }

        public IAggregateRoot Convert(BuildDto source, IAggregateRoot destination = default, ResolutionContext context = default)
        {
            var capabilityIdentifier = MatchCapabilityIdentifier(source.Tags);
            var buildDef = new BuildDefinition(source.Definition.Name, string.Empty, source.Definition.Id);
            var buildRoot = new BuildRoot(source.Project.Id, capabilityIdentifier, buildDef, source.Tags?.Select(o => new Tag(o)));

            return buildRoot;
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
