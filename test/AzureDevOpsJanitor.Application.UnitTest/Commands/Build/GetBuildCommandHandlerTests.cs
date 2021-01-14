using AzureDevOpsJanitor.Application.Commands.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Services;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Commands.Build
{
    public class GetBuildCommandHandlerTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            GetBuildCommandHandler sut;
            var mockBuildService = new Mock<IBuildService>();

            //Act
            sut = new GetBuildCommandHandler(mockBuildService.Object);

            //Assert
            Assert.NotNull(sut);
        }

        [Fact]
        public async Task CanHandleCommand()
        {
            //Arrange
            var mockBuildService = new Mock<IBuildService>();
            var sut = new GetBuildCommandHandler(mockBuildService.Object);

            mockBuildService.Setup(m => m.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(Enumerable.Empty<BuildRoot>()));

            //Act
            var result = await sut.Handle(new GetBuildCommand(1, Guid.NewGuid()));

            //Assert
            Assert.True(result != null);
            Assert.Empty(result);

            mockBuildService.VerifyAll();
        }
    }
}
