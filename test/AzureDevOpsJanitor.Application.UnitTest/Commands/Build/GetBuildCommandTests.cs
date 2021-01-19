using AzureDevOpsJanitor.Application.Commands.Build;
using System;
using System.Text.Json;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Commands.Build
{
    public class GetBuildCommandTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var sut = new GetBuildCommand(1, Guid.NewGuid());

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetHashCode());
            Assert.True(sut.ProjectId != Guid.Empty);
            Assert.Equal(1, sut.BuildId);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new GetBuildCommand(1, Guid.NewGuid());

            //Act
            var json = JsonSerializer.Serialize(sut);

            //Assert
            Assert.False(string.IsNullOrEmpty(json));
        }

        [Fact]
        public void CanBeDeserialized()
        {
            //Arrange
            GetBuildCommand sut;
            var json = "{\"projectId\":\"6def4aee-1467-4613-9b1b-5c4bf4cbbe89\",\"buildId\":1}";

            //Act
            sut = JsonSerializer.Deserialize<GetBuildCommand>(json);

            //Assert
            Assert.NotNull(sut);
            Assert.Equal("6def4aee-1467-4613-9b1b-5c4bf4cbbe89", sut.ProjectId.ToString());
            Assert.Equal(1, sut.BuildId);
        }
    }
}
