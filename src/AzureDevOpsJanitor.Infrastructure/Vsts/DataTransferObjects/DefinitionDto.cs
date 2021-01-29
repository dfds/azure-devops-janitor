﻿using System;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects
{
    public sealed class DefinitionDto : VstsDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("project")]
        public ProjectDto Project { get; set; }

        [JsonPropertyName("revision")]
        public int Revision { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("uri")]
        public Uri Uri { get; set; }

        [JsonPropertyName("queueStatus")]
        public string QueueStatus { get; set; }
    }
}