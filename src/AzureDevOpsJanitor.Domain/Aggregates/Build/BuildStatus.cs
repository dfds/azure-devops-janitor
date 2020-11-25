using ResourceProvisioning.Abstractions.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AzureDevOpsJanitor.Domain.Aggregates.Build
{
	public sealed class BuildStatus : EntityEnumeration
	{
		public static BuildStatus Created = new BuildStatus(1, nameof(Created).ToLowerInvariant());
		public static BuildStatus Queued = new BuildStatus(2, nameof(Queued).ToLowerInvariant());
		public static BuildStatus Succeeded = new BuildStatus(4, nameof(Succeeded).ToLowerInvariant());
		public static BuildStatus Failed = new BuildStatus(8, nameof(Failed).ToLowerInvariant());
		public static BuildStatus Stopped = new BuildStatus(16, nameof(Stopped).ToLowerInvariant());
		public static BuildStatus Partial = new BuildStatus(32, nameof(Partial).ToLowerInvariant());

		public BuildStatus(int id, string name) : base(id, name)
		{
		}

		public static IEnumerable<BuildStatus> List()
		{
			var result = new List<BuildStatus>
			{
				Created,
				Queued,
				Succeeded,
				Failed,
				Stopped,
				Partial
			};

			return result.AsEnumerable();
		}
	}
}
