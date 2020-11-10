using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.ValueObjects;
using ResourceProvisioning.Abstractions.Commands;
using System.Runtime.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
	[DataContract]
	public sealed class CreateBuildCommand : ICommand<BuildRoot>
	{
		[DataMember]
		public string CapabilityId { get; private set; }

		[DataMember]
		public BuildDefinition Definition { get; private set; }

		public CreateBuildCommand(string capabilityId, BuildDefinition definition)
		{
			CapabilityId = capabilityId;
			Definition = definition;
		}
	}
}
