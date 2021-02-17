using CloudEngineering.CodeOps.Abstractions.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AzureDevOpsJanitor.Domain.ValueObjects
{
    public sealed class Tag : ValueObject
    {
        [Required]
        [JsonPropertyName("value")]
        public string Value { get; private set; }

        private Tag() { }

        [JsonConstructor]
        public Tag(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}