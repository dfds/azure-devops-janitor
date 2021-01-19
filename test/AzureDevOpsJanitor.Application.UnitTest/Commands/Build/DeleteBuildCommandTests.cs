using AzureDevOpsJanitor.Application.Commands.Build;
using System.Text.Json;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Commands.Build
{
    public class DeleteBuildCommandTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var sut = new DeleteBuildCommand(1);

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetHashCode());
            Assert.Equal(1, sut.BuildId);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new DeleteBuildCommand(1);

            //Act
            var json = JsonSerializer.Serialize(sut);

            //Assert
            Assert.False(string.IsNullOrEmpty(json));
        }

        [Fact]
        public void CanBeDeserialized()
        {
            //Arrange
            DeleteBuildCommand sut;
            var json = "{\"buildId\":1}";

            //Act
            sut = JsonSerializer.Deserialize<DeleteBuildCommand>(json);

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(1, sut.BuildId);
        }
    }
}
