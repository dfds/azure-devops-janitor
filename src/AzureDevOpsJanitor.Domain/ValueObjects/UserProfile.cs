using ResourceProvisioning.Abstractions.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureDevOpsJanitor.Domain.ValueObjects
{
	public sealed class UserProfile : ValueObject
	{
		[Required]
		public string Name { get; private set; }

		private UserProfile() { }

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