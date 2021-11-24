using AutoMapper;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.ValueObjects;
using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Infrastructure.Azure.DevOps.DataTransferObjects.Build;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AzureDevOpsJanitor.Application.Mapping.Converters
{
    public class BuildRootToBuildDtoConverter : ITypeConverter<BuildRoot, BuildDto>, ITypeConverter<BuildDto, IAggregateRoot>
    {
        private const string CapabilityIdentifierRegEx = @"capability_id=(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}";

        public BuildDto Convert(BuildRoot source, BuildDto destination = default, ResolutionContext context = default)
        {
            throw new NotImplementedException();
        }

        public IAggregateRoot Convert(BuildDto source, IAggregateRoot destination = default, ResolutionContext context = default)
        {
            var capabilityIdentifier = MatchCapabilityIdentifier(source.Tags);

            if (capabilityIdentifier == string.Empty)
            {
                return null;
            }

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
                    return tag.Split("=").Last();
                }
            }

            return string.Empty;
        }
    }
}
