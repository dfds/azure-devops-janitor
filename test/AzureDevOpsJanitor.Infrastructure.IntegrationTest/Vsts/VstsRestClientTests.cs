using AzureDevOpsJanitor.Infrastructure.Vsts;
using Microsoft.Extensions.Configuration;
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
    
        [Fact(Skip = "Fix issue with PAT and AccessToken")]
        public async Task CanGetProfile()
        {
            //Arrange
            var jwtToken = new JwtSecurityToken(_fixture.Configuration.GetValue<string>("Vsts:ClientAccessToken"));
            var sut = new VstsRestClient(jwtToken);

            //Act
            var profile = await sut.GetProfile("me");

            //Assert
            Assert.NotNull(profile);
        }
    }
}
