using AzureDevOpsJanitor.Infrastructure.Vsts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Xunit;

namespace AzureDevOpsJanitor.Infrastructure.IntegrationTest.Vsts
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
            var options = new VstsRestClientOptions()
            {
                ClientSecret = _fixture.Configuration.GetValue<string>("Vsts:ClientAccessToken")
            };

            var sut = new VstsRestClient(Options.Create(options));

            //Act
            var projects = await sut.GetProjects("dfds");

            //Assert
            Assert.NotNull(projects);
        }
    }
}
