using System;

namespace AzureDevOpsJanitor.Domain
{
    public sealed class AzureDevOpsJanitorDomainException : Exception
    {
        public AzureDevOpsJanitorDomainException()
        { }

        public AzureDevOpsJanitorDomainException(string message)
            : base(message)
        { }

        public AzureDevOpsJanitorDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
