using AzureDevOpsJanitor.Application.Commands.Profile;
using AzureDevOpsJanitor.Application.Commands.Project;
using System;
using System.Text.Json;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Commands.Project
{
    public class GetProjectCommandTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            GetProjectCommand sut;

            //Act
            sut = new GetProjectCommand(Guid.NewGuid());

            //Assert
            Assert.NotNull(sut);
            Assert.True(sut.ProjectId != Guid.Empty);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new GetProjectCommand(Guid.NewGuid());

            //Act
            var json = JsonSerializer.Serialize(sut);

            //Assert
            Assert.False(string.IsNullOrEmpty(json));
        }

        [Fact]
        public void CanBeDeserialized()
        {
            //Arrange
            GetProjectCommand sut;
            var json = "{\"projectId\":\"6def4aee-1467-4613-9b1b-5c4bf4cbbe89\"}";

            //Act
            sut = JsonSerializer.Deserialize<GetProjectCommand>(json);

            //Assert
            Assert.NotNull(sut);
            Assert.Equal("6def4aee-1467-4613-9b1b-5c4bf4cbbe89", sut.ProjectId.ToString());
        }
    }
}
