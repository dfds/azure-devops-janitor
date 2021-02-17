using AzureDevOpsJanitor.Domain.Events.Project;
using CloudEngineering.CodeOps.Abstractions.Aggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureDevOpsJanitor.Domain.Aggregates.Project
{
    public sealed class ProjectRoot : AggregateRoot<Guid>
    {
        public string Name { get; private set; }

        public ProjectRoot(string name)
        {
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
