using ResourceProvisioning.Abstractions.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureDevOpsJanitor.Domain.ValueObjects
{
	public class Profile : ValueObject
	{
		[Required]
		public string Name { get; protected set; }

		protected Profile() { }

		public Profile(string name)
		{
			Name = name;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Name;
		}
	}
}