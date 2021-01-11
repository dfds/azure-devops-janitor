using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Entities;

namespace AzureDevOpsJanitor.Domain.Aggregates
{
    public abstract class AbstractRoot<TKey> : Entity<TKey>, IAggregateRoot where TKey : struct
    {

    }
}
