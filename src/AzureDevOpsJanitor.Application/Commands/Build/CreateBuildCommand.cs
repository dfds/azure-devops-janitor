using AzureDevOpsJanitor.Domain.Aggregates.Build;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using System;
using System.Runtime.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
	[DataContract]
	public sealed class CreateBuildCommand : ICommand<BuildRoot>, IProvisioningRequest
	{
		[DataMember]
		public string CapabilityId { get; private set; }

		public CreateBuildCommand(string capabilityId)
		{
			CapabilityId = capabilityId;
		}
	}
}
