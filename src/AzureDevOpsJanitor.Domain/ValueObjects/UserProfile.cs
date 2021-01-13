using ResourceProvisioning.Abstractions.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Domain.ValueObjects
{
	public sealed class UserProfile : ValueObject
	{
		[Required]
		[JsonPropertyName("name")]
		public string Name { get; private set; }

		private UserProfile() { }

		[JsonConstructor]
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