using AzureDevOpsJanitor.Application.Commands.Build;
using AzureDevOpsJanitor.Domain.Services;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Commands.Build
{
    public class DeleteBuildCommandHandlerTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var mockBuildService = new Mock<IBuildService>();
            var sut = new DeleteBuildCommandHandler(mockBuildService.Object);

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.Equal(hashCode, sut.GetHashCode());
            Assert.NotNull(sut);

            Mock.VerifyAll();
        }

        [Fact]
        public async Task CanHandleCommand()
        {
            //Arrange
            var buildId = 1;
            var mockBuildService = new Mock<IBuildService>();
            var sut = new DeleteBuildCommandHandler(mockBuildService.Object);

            mockBuildService.Setup(m => m.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));

            //Act
            var result = await sut.Handle(new DeleteBuildCommand(buildId));

            //Assert
            Assert.True(result);

            Mock.VerifyAll();
        }
    }
}
