using ResourceProvisioning.Abstractions.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureDevOpsJanitor.Domain.ValueObjects
{
	public class BuildDefinition : ValueObject
	{
		[Required]
		public string Name { get; protected set; }

		[Required]
		public string Yaml { get; protected set; }

		protected BuildDefinition() { }

		public BuildDefinition(string name, string yaml)
		{
			Name = name;
			Yaml = yaml;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Name;
			yield return Yaml;
		}
	}
}