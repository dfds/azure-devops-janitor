using ResourceProvisioning.Abstractions.Commands;
using System.Runtime.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
	[DataContract]
	public sealed class DeleteBuildCommand : ICommand<bool>
	{
		[DataMember]
		public ulong BuildId { get; private set; }

		public DeleteBuildCommand(ulong buildId)
		{
			BuildId = buildId;
		}
	}
}
