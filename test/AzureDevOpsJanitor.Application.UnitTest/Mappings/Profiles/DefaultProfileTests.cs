using AzureDevOpsJanitor.Application.Mapping.Profiles;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Mapping.Profiles
{
    public class DefaultProfileTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            DefaultProfile sut;

            //Act
            sut = new DefaultProfile();

            //Assert
            Assert.NotNull(sut);
        }
    }
}
