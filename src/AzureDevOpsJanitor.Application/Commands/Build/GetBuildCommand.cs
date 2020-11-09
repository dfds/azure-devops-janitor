using AzureDevOpsJanitor.Domain.Aggregates.Build;
using ResourceProvisioning.Abstractions.Commands;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
	[DataContract]
	public sealed class GetBuildCommand : ICommand<IEnumerable<BuildRoot>>
	{
		[DataMember]
		public ulong? BuildId { get; private set; }

		public GetBuildCommand(ulong? buildId = null)
		{
			BuildId = buildId;
		}
	}
}
