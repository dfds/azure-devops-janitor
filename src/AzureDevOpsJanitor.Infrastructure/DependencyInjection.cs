using CloudEngineering.CodeOps.Infrastructure.AzureDevOps;
using CloudEngineering.CodeOps.Infrastructure.Kafka;
using CloudEngineering.CodeOps.Security.Policies;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureDevOpsJanitor.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //External dependencies
            services.AddTransient<ServiceFactory>(p => p.GetService);
            services.AddTransient<IMediator>(p => new Mediator(p.GetService<ServiceFactory>()));
            services.AddAzureDevOps(configuration);
            services.AddKafka(configuration);
            services.AddSecurityPolicies();
        }
    }
}
