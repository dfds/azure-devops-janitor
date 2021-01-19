using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using System.Text.Json;
using Xunit;

namespace AzureDevOpsJanitor.Infrastructure.UnitTest.Vsts.Events
{
    public class BuildDtoTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            BuildDto sut;

            //Act
            sut = new BuildDto();

            //Assert
            Assert.NotNull(sut);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new BuildDto()
            {
                Id = 1,
                Project = "MyProject",
                BuildNumber = "1234"
            };

            //Act
            var payload = JsonSerializer.Serialize(sut, new JsonSerializerOptions { IgnoreNullValues = true });

            //Assert
            Assert.NotNull(JsonDocument.Parse(payload));
        }

        [Fact]
        public void CanBeDeserialized()
        {
            //Arrange
            BuildDto sut;

            //Act
            sut = JsonSerializer.Deserialize<BuildDto>("{\"id\":1,\"project\":\"MyProject\",\"buildNumber\":\"1234\"}");

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(1, sut.Id);
            Assert.Equal("MyProject", sut.Project);
            Assert.Equal("1234", sut.BuildNumber);
        }
    }
}