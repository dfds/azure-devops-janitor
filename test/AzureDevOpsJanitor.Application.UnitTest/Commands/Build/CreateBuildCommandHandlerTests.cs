using AzureDevOpsJanitor.Application.Commands.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Services;
using AzureDevOpsJanitor.Domain.ValueObjects;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Commands.Build
{
    public class CreateBuildCommandHandlerTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            CreateBuildCommandHandler sut;
            var mockBuildService = new Mock<IBuildService>();

            //Act
            sut = new CreateBuildCommandHandler(mockBuildService.Object);

            //Assert
            Assert.NotNull(sut);
        }

        [Fact]
        public async Task CanHandleCommand()
        {
            //Arrange
            var mockBuildService = new Mock<IBuildService>();
            var buildRoot = new BuildRoot(Guid.NewGuid(), "my-capability-identifier-or-guid", new BuildDefinition("name", "yaml", 1));
            var sut = new CreateBuildCommandHandler(mockBuildService.Object);

            mockBuildService.Setup(m => m.AddAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<BuildDefinition>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(buildRoot));
            mockBuildService.Setup(m => m.QueueAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));

            //Act
            var result = await sut.Handle(new CreateBuildCommand(Guid.NewGuid(), Guid.NewGuid().ToString(), new BuildDefinition("name", "yaml", 1)));

            //Assert
            Assert.Equal(result, buildRoot);
            mockBuildService.VerifyAll();
        }
    }
}
