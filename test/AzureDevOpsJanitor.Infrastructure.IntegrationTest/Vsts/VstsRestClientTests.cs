using AzureDevOpsJanitor.Infrastructure.Vsts;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
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
