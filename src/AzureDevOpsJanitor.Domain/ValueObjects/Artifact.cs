using CloudEngineering.CodeOps.Abstractions.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Domain.ValueObjects
{
    public sealed class Artifact : ValueObject
    {
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; private set; }

        private Artifact() { }

        [JsonConstructor]
        public Artifact(string name)
        {
            Name = name;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
        }
    }
}