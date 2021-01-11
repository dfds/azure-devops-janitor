﻿using AzureDevOpsJanitor.Domain.Events.Build;
using AzureDevOpsJanitor.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureDevOpsJanitor.Domain.Aggregates.Build
{
	public sealed class BuildRoot : AbstractRoot<int>
	{
		private BuildStatus _status;
#pragma warning disable IDE0052 // Remove unread private members
		private int _statusId;
		private readonly string _capabilityIdentifier;
#pragma warning restore IDE0052 // Remove unread private members

        public Guid ProjectId { get; private set; }

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

		private BuildRoot() : base()
		{
			Status = BuildStatus.Created;

			AddDomainEvent(new BuildCreatedEvent(this));
		}

		public BuildRoot(Guid projectId, string capabilityIdentifier, BuildDefinition definition) : this()
		{
			_capabilityIdentifier = capabilityIdentifier;
			ProjectId = projectId;
			Definition = definition;
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
			if (Status != BuildStatus.Created)
			{
				return;
			}

			Status = BuildStatus.Succeeded;

			AddDomainEvent(new BuildCompletedEvent(this));
		}

		public void Failed()
		{
			if (Status != BuildStatus.Created)
			{
				return;
			}

			Status = BuildStatus.Failed;

			AddDomainEvent(new BuildCompletedEvent(this));
		}

		public void Stopped()
		{
			if (Status != BuildStatus.Created)
			{
				return;
			}

			Status = BuildStatus.Stopped;

			AddDomainEvent(new BuildCompletedEvent(this));
		}

		public void Partial()
		{
			if (Status != BuildStatus.Created)
			{
				return;
			}

			Status = BuildStatus.Partial;

			AddDomainEvent(new BuildCompletedEvent(this));
		}

		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (ProjectId == Guid.Empty)
			{
				yield return new ValidationResult(nameof(ProjectId));
			}

			if (!Guid.TryParse(_capabilityIdentifier, out _) || string.IsNullOrEmpty(_capabilityIdentifier))
			{
				yield return new ValidationResult(nameof(_capabilityIdentifier));
			}
		}
	}
}
