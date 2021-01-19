using AutoMapper;
using AzureDevOpsJanitor.Application.Commands.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Commands;

namespace AzureDevOpsJanitor.Application.Mapping.Converters
{
    public class AggregateRootToCommandConverter : ITypeConverter<IAggregateRoot, ICommand<IAggregateRoot>>
    {
        public ICommand<IAggregateRoot> Convert(IAggregateRoot source, ICommand<IAggregateRoot> destination, ResolutionContext context)
        {
            switch (source)
            {
                case BuildRoot build:
                    if(build.Id == 0)
                    { 
                        return new CreateBuildCommand(build.ProjectId, build.CapabilityIdentifier, build.Definition);
                    }

                    break;
                case null:
                default:
                    break;
            }

            return null;
        }
    }
}
