using AutoMapper;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using System.Linq;

namespace AzureDevOpsJanitor.Application.Mapping.Converters
{
    public class BuildRootToBuildDefinitionDtoConverter : ITypeConverter<BuildRoot, BuildDefinitionDto>
    {
        private readonly IMapper _mapper;

        public BuildRootToBuildDefinitionDtoConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public BuildDefinitionDto Convert(BuildRoot source, BuildDefinitionDto destination = default, ResolutionContext context = default)
        {
            var result = _mapper.Map<BuildDefinitionDto>(source.Definition);

            result.Tags = source.Tags.Select(tag => tag.Value);

            return result;
        }
    }
}
