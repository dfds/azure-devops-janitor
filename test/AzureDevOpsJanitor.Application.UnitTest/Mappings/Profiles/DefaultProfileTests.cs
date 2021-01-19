using AzureDevOpsJanitor.Application.Mappings.Profiles;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Mappings.Profiles
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
