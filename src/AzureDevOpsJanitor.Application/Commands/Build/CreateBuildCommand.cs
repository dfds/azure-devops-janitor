using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.ValueObjects;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Runtime.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
	[DataContract]
	public sealed class CreateBuildCommand : ICommand<BuildRoot>
	{
		[DataMember]
		public Guid ProjectId { get; private set; }

		[DataMember]
		public string CapabilityId { get; private set; }

		[DataMember]
		public BuildDefinition Definition { get; private set; }

		public CreateBuildCommand(Guid projectId, string capabilityId, BuildDefinition definition)
		{
			ProjectId = projectId;
			CapabilityId = capabilityId;
			Definition = definition;
		}
	}
}
