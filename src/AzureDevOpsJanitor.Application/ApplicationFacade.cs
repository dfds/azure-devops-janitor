using CloudEngineering.CodeOps.Abstractions.Facade;
using Microsoft.Extensions.DependencyInjection;

namespace AzureDevOpsJanitor.Application
{
    public sealed class ApplicationFacade : Facade, IApplicationFacade
    {
        public ApplicationFacade(IServiceScopeFactory scopeFactory) : base(scopeFactory)
        {

        }
    }
}
