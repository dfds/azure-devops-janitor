using AzureDevOpsJanitor.Domain.Events.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureDevOpsJanitor.Domain.Aggregates.Project
{
	//TODO: Finalize aggregate
	public sealed class ProjectRoot : AbstractRoot<Guid>
	{
		public string Name { get; private set; }

		public ProjectRoot(string name)
		{
			Id = Guid.NewGuid();
			Name = name;

			AddDomainEvent(new ProjectCreatedEvent(this));
		}

		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (string.IsNullOrEmpty(Name))
			{
				yield return new ValidationResult(nameof(Name));
			}
		}
	}
}
