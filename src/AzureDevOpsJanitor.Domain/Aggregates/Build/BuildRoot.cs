using AzureDevOpsJanitor.Domain.Events.Build;
using AzureDevOpsJanitor.Domain.ValueObjects;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureDevOpsJanitor.Domain.Aggregates.Build
{
	//TODO: Finish aggregate
	public sealed class BuildRoot : Entity<ulong>, IAggregateRoot
	{
		private BuildStatus _status;
#pragma warning disable IDE0052 // Remove unread private members
		private int _statusId;
		private readonly string _capabilityId;
#pragma warning restore IDE0052 // Remove unread private members

		public BuildDefinition Definition { get; private set; }

		public BuildStatus Status { 
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

		private BuildRoot()
		{
			Status = BuildStatus.Requested;

			AddDomainEvent(new BuildRequestedEvent(this));
		}

		public BuildRoot(string capabilityId, BuildDefinition definition) : base()
		{
			_capabilityId = capabilityId;
			Definition = definition;
		}

		public void Initialized()
		{
			if (Status != BuildStatus.Requested)
			{
				return;
			}

			Status = BuildStatus.Initialized;

			AddDomainEvent(new BuildInitializedEvent(Id));
		}

		public void Succeeded()
		{
			if (Status != BuildStatus.Initialized)
			{
				return;
			}

			Status = BuildStatus.Succeeded;

			AddDomainEvent(new BuildCompletedEvent(Id, Status));
		}

		public void Failed()
		{
			if (Status != BuildStatus.Initialized)
			{
				return;
			}

			Status = BuildStatus.Failed;

			AddDomainEvent(new BuildCompletedEvent(Id, Status));
		}

		public void Stopped()
		{
			if (Status != BuildStatus.Initialized)
			{
				return;
			}

			Status = BuildStatus.Stopped;

			AddDomainEvent(new BuildCompletedEvent(Id, Status));
		}

		public void Partial()
		{
			if (Status != BuildStatus.Initialized)
			{
				return;
			}

			Status = BuildStatus.Partial;

			AddDomainEvent(new BuildCompletedEvent(Id, Status));
		}

		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (!Guid.TryParse(_capabilityId, out _) || !string.IsNullOrEmpty(_capabilityId))
			{
				yield return new ValidationResult(nameof(_capabilityId));
			}
		}
	}
}
