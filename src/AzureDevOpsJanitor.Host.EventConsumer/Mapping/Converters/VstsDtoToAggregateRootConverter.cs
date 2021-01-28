using AutoMapper;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using ResourceProvisioning.Abstractions.Aggregates;

namespace AzureDevOpsJanitor.Host.EventConsumer.Mapping.Converters
{
    public class VstsDtoToAggregateRootConverter : ITypeConverter<VstsDto, IAggregateRoot>
    {
        public IAggregateRoot Convert(VstsDto source, IAggregateRoot destination, ResolutionContext context)
        {
            return null;
        }
    }
}
