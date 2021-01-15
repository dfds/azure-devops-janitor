using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace AzureDevOpsJanitor.Infrastructure.IntegrationTest.Vsts
{
    public class ConfigurationFixture : IDisposable
    {
        public IConfiguration Configuration { get; init; }

        public ConfigurationFixture()
        {
            Configuration = new ConfigurationBuilder()
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .Build();
        }

        public void Dispose()
        {
        }
    }
}
