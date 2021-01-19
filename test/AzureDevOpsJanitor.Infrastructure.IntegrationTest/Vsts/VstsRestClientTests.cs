using AzureDevOpsJanitor.Infrastructure.Vsts;
using Microsoft.Extensions.Configuration;
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
            var patString = _fixture.Configuration.GetValue<string>("Vsts:ClientAccessToken");
            var sut = new VstsRestClient(patString);

            //Act
            var projects = await sut.GetProjects("dfds");

            //Assert
            Assert.NotNull(projects);
        }
    }
}
