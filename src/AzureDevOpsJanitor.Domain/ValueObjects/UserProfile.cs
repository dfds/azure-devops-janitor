using ResourceProvisioning.Abstractions.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureDevOpsJanitor.Domain.ValueObjects
{
	public class UserProfile : ValueObject
	{
		[Required]
		public string Name { get; protected set; }

		protected UserProfile() { }

		public UserProfile(string name)
		{
			Name = name;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Name;
		}
	}
}