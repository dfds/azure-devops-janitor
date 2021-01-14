using AzureDevOpsJanitor.Application.Commands.Profile;
using System.Text.Json;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Commands.Profile
{
    public class GetProfileCommandTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var sut = new GetProfileCommand("my-profile");

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetHashCode());
            Assert.Equal("my-profile", sut.ProfileIdentifier);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new GetProfileCommand("my-profile");

            //Act
            var json = JsonSerializer.Serialize(sut);

            //Assert
            Assert.False(string.IsNullOrEmpty(json));
        }

        [Fact]
        public void CanBeDeserialized()
        {
            //Arrange
            GetProfileCommand sut;
            var json = "{\"profileIdentifier\":\"my-profile\"}";

            //Act
            sut = JsonSerializer.Deserialize<GetProfileCommand>(json);

            //Assert
            Assert.NotNull(sut);
            Assert.Equal("my-profile", sut.ProfileIdentifier);
        }
    }
}
