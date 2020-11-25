using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AzureDevOpsJanitor.Domain.Aggregates
{
    public abstract class AbstractRoot<TKey> : Entity<TKey>, IAggregateRoot where TKey : struct
    {
        protected AbstractRoot() : base()
        {
            var validationResult = Validate(new ValidationContext(this));

            if (validationResult.Any())
            {
                throw new AggregateException(validationResult.Select(result => new ArgumentException(result.ErrorMessage)));
            }
        }
    }
}
