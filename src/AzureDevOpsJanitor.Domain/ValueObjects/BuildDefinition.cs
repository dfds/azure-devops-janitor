using ResourceProvisioning.Abstractions.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureDevOpsJanitor.Domain.ValueObjects
{
	public sealed class BuildDefinition : ValueObject
	{
		[Required]
		public int DefinitionId { get; private set; }

		[Required]
		public string Name { get; private set; }

		[Required]
		public string Yaml { get; private set; }

		private BuildDefinition() { }

		public BuildDefinition(string name, string yaml, int definitionId)
		{
			Name = name;
			Yaml = yaml;
			DefinitionId = definitionId;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Name;
			yield return Yaml;
			yield return DefinitionId;
		}
	}
}