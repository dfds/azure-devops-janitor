using AzureDevOpsJanitor.Infrastructure.EntityFramework;
using Xunit;

namespace AzureDevOpsJanitor.Infrastructure.IntegrationTest.EntityFramework
{
    public class DomainContextTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var sut = new DomainContext();

            //Act
            var hashCode = sut.GetType().GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetType().GetHashCode());
        }
    }
}
