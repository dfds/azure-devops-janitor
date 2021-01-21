using System;

namespace AzureDevOpsJanitor.Infrastructure.Vsts
{
    public sealed class VstsRestClientException : Exception
    {
        public VstsRestClientException()
        { }

        public VstsRestClientException(string message)
            : base(message)
        { }

        public VstsRestClientException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
