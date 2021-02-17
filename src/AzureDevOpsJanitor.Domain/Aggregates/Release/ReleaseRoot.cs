using AzureDevOpsJanitor.Domain.Events.Release;
using AzureDevOpsJanitor.Domain.ValueObjects;
using CloudEngineering.CodeOps.Abstractions.Aggregates;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AzureDevOpsJanitor.Domain.Aggregates.Release
{
    public sealed class ReleaseRoot : AggregateRoot<int>
    {
        private readonly List<Artifact> _artifacts;
        private readonly List<BuildEnvironment> _environments;

        public string Name { get; private set; }

        public IEnumerable<Artifact> Artifacts => _artifacts.AsReadOnly();

        public IEnumerable<BuildEnvironment> Environments => _environments.AsReadOnly();

        private ReleaseRoot() : base()
        {
            AddDomainEvent(new ReleaseCreatedEvent(this));
        }

        public ReleaseRoot(string name, IEnumerable<Artifact> artifacts = default, IEnumerable<BuildEnvironment> environments = default) : this()
        {
            Name = name;
            _artifacts = artifacts.ToList() ?? new List<Artifact>();
            _environments = environments.ToList() ?? new List<BuildEnvironment>();
        }

        public void AddArtifact(Artifact artifact)
        {
            _artifacts.Add(artifact);
        }

        public void AddBuildEnvironment(BuildEnvironment environment)
        {
            _environments.Add(environment);
        }

        public void RemoveArtifact(Artifact artifact)
        {
            _artifacts.Add(artifact);
        }

        public void RemoveBuildEnvironment(BuildEnvironment environment)
        {
            _environments.Add(environment);
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
