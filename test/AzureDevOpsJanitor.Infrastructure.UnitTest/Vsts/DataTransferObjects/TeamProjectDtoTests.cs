using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using System;
using System.Text.Json;
using Xunit;

namespace AzureDevOpsJanitor.Infrastructure.UnitTest.Vsts.Events
{
    public class TeamProjectDtoTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            TeamProjectDto sut;

            //Act
            sut = new TeamProjectDto();

            //Assert
            Assert.NotNull(sut);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new TeamProjectDto() { 
                Id = 1,
                Name = "MyName",
                Description = "MyDescription",
                State = "MyState",
                Url = new Uri("https://my-team-project-url")
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
            TeamProjectDto sut;

            //Act
            sut = JsonSerializer.Deserialize<TeamProjectDto>("{\"id\":1,\"name\":\"MyName\",\"description\":\"MyDescription\",\"state\":\"MyState\",\"url\":\"https://my-team-project-url\"}");

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(1, sut.Id);
            Assert.Equal("MyName", sut.Name);
            Assert.Equal("MyDescription", sut.Description);
            Assert.Equal("MyState", sut.State);
            Assert.Equal("https://my-team-project-ur", sut.Url.AbsoluteUri);
        }
    }
}