using CloudEngineering.CodeOps.Infrastructure.AzureDevOps;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Xunit;

namespace CloudEngineering.CodeOps.Infrastructure.IntegrationTest.Vsts
{
    public class VstsRestClientTests : IClassFixture<ConfigurationFixture>
    {
        private readonly ConfigurationFixture _fixture;

        public VstsRestClientTests(ConfigurationFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CanGetProjects()
        {
            //Arrange
            var options = new AdoClientOptions()
            {
                ClientSecret = _fixture.Configuration.GetValue<string>("Vsts:ClientAccessToken")
            };

            var sut = new AdoClient(Options.Create(options));

            //Act
            var projects = await sut.GetProjects("dfds");

            //Assert
            Assert.NotNull(projects);
        }
    }
}
