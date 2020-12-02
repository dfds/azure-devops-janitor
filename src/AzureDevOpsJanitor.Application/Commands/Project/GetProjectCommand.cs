using AzureDevOpsJanitor.Domain.Aggregates.Project;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AzureDevOpsJanitor.Application.Commands.Project
{
	[DataContract]
	public sealed class GetProjectCommand : ICommand<IEnumerable<ProjectRoot>>
	{
		[DataMember]
		public Guid? ProjectId { get; private set; }

		public GetProjectCommand(Guid? projectId = default)
		{
			ProjectId = projectId;
		}
	}
}
