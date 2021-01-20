using System;

namespace AzureDevOpsJanitor.Infrastructure.EntityFramework
{
    public sealed class DomainContextException : Exception
    {
        public DomainContextException()
        { }

        public DomainContextException(string message)
            : base(message)
        { }

        public DomainContextException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
