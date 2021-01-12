using AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build.Definition;
using System.Net.Http;
using Xunit;

namespace AzureDevOpsJanitor.Infrastructure.UnitTest.Vsts.Http.Request.Build.Definition
{
    public class GetDefinitionRequestTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            GetDefinitionRequest sut;

            //Act
            sut = new GetDefinitionRequest("my-org", "my-project", 1);

            //Assert
            Assert.NotNull(sut);
            Assert.Equal("6.1-preview.7", sut.ApiVersion);
            Assert.Equal(HttpMethod.Get, sut.Method);
            Assert.Equal("https://dev.azure.com/my-org/my-project/_apis/build/definitions/1?api-version=6.1-preview.7", sut.RequestUri.AbsoluteUri);
        }
    }
}