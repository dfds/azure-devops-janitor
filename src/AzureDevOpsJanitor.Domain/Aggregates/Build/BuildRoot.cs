using AzureDevOpsJanitor.Domain.Events.Build;
using AzureDevOpsJanitor.Domain.ValueObjects;
using ResourceProvisioning.Abstractions.Aggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AzureDevOpsJanitor.Domain.Aggregates.Build
{
    public sealed class BuildRoot : AggregateRoot<int>
    {
        private BuildStatus _status;
#pragma warning disable IDE0052 // Remove unread private members
        private int _statusId;
#pragma warning restore IDE0052 // Remove unread private members
        
        private readonly List<Tag> _tags;
        
        public string CapabilityIdentifier { get; init; }

        public Guid ProjectId { get; private set; }

        public BuildDefinition Definition { get; private set; }

        public BuildStatus Status
        {
            get
            {
                return _status;
            }
            private set
            {
                _status = value;
                _statusId = _status.Id;
            }
        }

        public IEnumerable<Tag> Tags => _tags.AsReadOnly();

        private BuildRoot() : base()
        {
            Status = BuildStatus.Created;
            _tags ??= new List<Tag>();

            AddDomainEvent(new BuildCreatedEvent(this));
        }

        public BuildRoot(Guid projectId, string capabilityIdentifier, BuildDefinition definition, IEnumerable<Tag> tags = default) : this()
        {
            CapabilityIdentifier = capabilityIdentifier;
            ProjectId = projectId;
            Definition = definition;
            _tags = tags?.ToList();
        }

        public void Queue()
        {
            if (Status != BuildStatus.Created)
            {
                return;
            }

            Status = BuildStatus.Queued;

            AddDomainEvent(new BuildQueuedEvent(this));
        }

        public void Succeeded()
        {
            if (Status != BuildStatus.Queued)
            {
                return;
            }

            Status = BuildStatus.Succeeded;

            AddDomainEvent(new BuildCompletedEvent(this));
        }

        public void Failed()
        {
            if (Status != BuildStatus.Queued)
            {
                return;
            }

            Status = BuildStatus.Failed;

            AddDomainEvent(new BuildCompletedEvent(this));
        }

        public void Stopped()
        {
            if (Status != BuildStatus.Queued)
            {
                return;
            }

            Status = BuildStatus.Stopped;

            AddDomainEvent(new BuildCompletedEvent(this));
        }

        public void Partial()
        {
            if (Status != BuildStatus.Queued)
            {
                return;
            }

            Status = BuildStatus.Partial;

            AddDomainEvent(new BuildCompletedEvent(this));
        }

        public void AddTag(Tag tag)
        {
            _tags.Add(tag);
        }

        public void RemoveTag(Tag tag)
        {
            _tags.Remove(tag);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProjectId == Guid.Empty)
            {
                yield return new ValidationResult(nameof(ProjectId));
            }

            if (!Guid.TryParse(CapabilityIdentifier, out _) || string.IsNullOrEmpty(CapabilityIdentifier))
            {
                yield return new ValidationResult(nameof(CapabilityIdentifier));
            }
        }
    }
}
