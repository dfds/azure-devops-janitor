using AzureDevOpsJanitor.Domain.Aggregates.Build;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Build
{
	[DataContract]
	public sealed class GetBuildCommand : ICommand<IEnumerable<BuildRoot>>
	{
		[DataMember]
		public int? BuildId { get; private set; }

		[DataMember]
		public Guid? ProjectId { get; private set; }

		public GetBuildCommand(int? buildId = default, Guid? projectId = default)
		{
			BuildId = buildId;
			ProjectId = projectId;
		}
	}
}
