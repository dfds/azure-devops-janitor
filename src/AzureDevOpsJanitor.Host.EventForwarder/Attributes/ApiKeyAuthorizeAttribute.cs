using System;
using AzureDevOpsJanitor.Host.EventForwarder.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AzureDevOpsJanitor.Host.EventForwarder.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthorizeAttribute : TypeFilterAttribute
    {
        public ApiKeyAuthorizeAttribute() : base(typeof(ApiKeyAuthorizeAsyncFilter))
        {
        }
    }
}