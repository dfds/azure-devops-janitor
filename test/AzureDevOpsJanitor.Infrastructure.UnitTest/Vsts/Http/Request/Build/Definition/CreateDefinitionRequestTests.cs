using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts.Http.Request.Build.Definition;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace AzureDevOpsJanitor.Infrastructure.UnitTest.Vsts.Http.Request.Build.Definition
{
    public class CreateDefinitionRequestTests
    {
        [Fact]
        public async Task CanBeConstructed()
        {
            //Arrange
            CreateDefinitionRequest sut;

            //Act
            sut = new CreateDefinitionRequest("my-org", "my-project", new DefinitionReferenceDto());

            //Assert
            Assert.NotNull(sut);
            Assert.Equal("6.1-preview.7", sut.ApiVersion);
            Assert.Equal(HttpMethod.Post, sut.Method);
            Assert.Equal("https://dev.azure.com/my-org/my-project/_apis/build/definitions?api-version=6.1-preview.7", sut.RequestUri.AbsoluteUri);
            Assert.True(await new StringContent(JsonSerializer.Serialize(new DefinitionReferenceDto())).ReadAsStringAsync() == await sut.Content.ReadAsStringAsync());
        }
    }
}