using AzureDevOpsJanitor.Domain.Aggregates.Build;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using System.Runtime.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
	[DataContract]
	public sealed class GetBuildCommand : ICommand<BuildRoot>, IProvisioningRequest
	{
		[DataMember]
		public ulong BuildId { get; private set; }

		public GetBuildCommand(ulong buildId)
		{
			BuildId = buildId;
		}
	}
}
