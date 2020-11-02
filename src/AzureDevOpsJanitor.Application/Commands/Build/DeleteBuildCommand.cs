using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using System.Runtime.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
	[DataContract]
	public sealed class DeleteBuildCommand : ICommand<bool>, IProvisioningRequest
	{
		[DataMember]
		public ulong BuildId { get; private set; }

		public DeleteBuildCommand(ulong buildId)
		{
			BuildId = buildId;
		}
	}
}
