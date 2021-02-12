using AutoMapper;
using AzureDevOpsJanitor.Domain.Aggregates.Release;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using ResourceProvisioning.Abstractions.Aggregates;

namespace AzureDevOpsJanitor.Application.Mapping.Converters
{
    public class ReleaseRootToReleaseDtoConverter : ITypeConverter<ReleaseRoot, ReleaseDto>, ITypeConverter<ReleaseDto, IAggregateRoot>
    {
        public IAggregateRoot Convert(ReleaseDto source, IAggregateRoot destination = default, ResolutionContext context = default)
        {
            var releaseRoot = new ReleaseRoot(source.Name);

            return releaseRoot;
        }

        public ReleaseDto Convert(ReleaseRoot source, ReleaseDto destination, ResolutionContext context)
        {
            var releaseDto = new ReleaseDto
            {
                Name = source.Name
            };

            return releaseDto;
        }
    }
}
