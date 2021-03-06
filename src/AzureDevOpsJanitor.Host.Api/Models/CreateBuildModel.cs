﻿using AzureDevOpsJanitor.Domain.ValueObjects;
using System;

namespace AzureDevOpsJanitor.Host.Api.Models
{
    public sealed class CreateBuildModel
    {
        public Guid ProjectId { get; set; }

        public string CapabilityIdentifier { get; set; }

        public BuildDefinition Definition { get; set; }
    }
}
