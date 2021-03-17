using CloudEngineering.CodeOps.Infrastructure.Azure.DevOps;
using CloudEngineering.CodeOps.Infrastructure.Kafka;
using CloudEngineering.CodeOps.Security.Policies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureDevOpsJanitor.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //External dependencies
            services.AddAzureDevOps(configuration);
            services.AddKafka(configuration);
            services.AddSecurityPolicies();
        }
    }
}
