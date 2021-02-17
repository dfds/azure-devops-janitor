using CloudEngineering.CodeOps.Abstractions.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Domain.ValueObjects
{
    public sealed class BuildDefinition : ValueObject
    {
        [Required]
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [Required]
        [JsonPropertyName("name")]
        public string Name { get; init; }

        [Required]
        [JsonPropertyName("yaml")]
        public string Yaml { get; init; }

        private BuildDefinition() { }

        [JsonConstructor]
        public BuildDefinition(string name, string yaml, int id)
        {
            Name = name;
            Yaml = yaml;
            Id = id;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Yaml;
            yield return Id;
        }
    }
}