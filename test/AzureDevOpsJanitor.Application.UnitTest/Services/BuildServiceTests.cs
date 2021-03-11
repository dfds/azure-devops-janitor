using AzureDevOpsJanitor.Application.Services;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Events.Build;
using AzureDevOpsJanitor.Domain.Repository;
using AzureDevOpsJanitor.Domain.ValueObjects;
using CloudEngineering.CodeOps.Abstractions.Data;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Services
{
    public class BuildServiceTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            BuildService sut;
            var mockBuildRepository = new Mock<IBuildRepository>();

            //Act
            sut = new BuildService(mockBuildRepository.Object);

            //Assert
            Assert.NotNull(sut);

            Mock.VerifyAll();
        }

        [Fact]
        public async Task CanAdd()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBuildRepository = new Mock<IBuildRepository>();
            var projectId = Guid.NewGuid();
            var buildRoot = new BuildRoot(projectId, "my-capability-identifier-or-guid", new BuildDefinition("name", "yaml", 1));

            mockUnitOfWork.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mockBuildRepository.SetupGet(m => m.UnitOfWork).Returns(mockUnitOfWork.Object);
            mockBuildRepository.Setup(m => m.Add(It.IsAny<BuildRoot>())).Returns(buildRoot);

            var sut = new BuildService(mockBuildRepository.Object);

            //Act
            var result = await sut.AddAsync(projectId, "my-capability-identifier-or-guid", new BuildDefinition("name", "yaml", 1));

            //Assert
            Assert.Equal(result, buildRoot);

            Mock.VerifyAll();
        }

        [Fact]
        public async Task CanDelete()
        {
            //Arrange
            var buildId = 1;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBuildRepository = new Mock<IBuildRepository>();
            var buildDefinition = new BuildDefinition("name", "yaml", buildId);
            var buildRoot = new BuildRoot(Guid.NewGuid(), "my-capability-identifier-or-guid", buildDefinition);

            mockUnitOfWork.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mockBuildRepository.SetupGet(m => m.UnitOfWork).Returns(mockUnitOfWork.Object);
            mockBuildRepository.Setup(m => m.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(buildRoot));
            mockBuildRepository.Setup(m => m.Delete(It.IsAny<BuildRoot>()));

            var sut = new BuildService(mockBuildRepository.Object);

            var x = await sut.GetAsync();
            //Act
            await sut.DeleteAsync(buildId);

            //Assert
            Mock.VerifyAll();
        }

        [Fact]
        public async Task CanQueue()
        {
            //Arrange
            var buildId = 1;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBuildRepository = new Mock<IBuildRepository>();
            var buildDefinition = new BuildDefinition("name", "yaml", buildId);
            var buildRoot = new BuildRoot(Guid.NewGuid(), "my-capability-identifier-or-guid", buildDefinition);

            mockUnitOfWork.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mockBuildRepository.SetupGet(m => m.UnitOfWork).Returns(mockUnitOfWork.Object);
            mockBuildRepository.Setup(m => m.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(buildRoot));

            var sut = new BuildService(mockBuildRepository.Object);

            //Act
            await sut.QueueAsync(buildId);

            //Assert
            Assert.Equal(2, buildRoot.DomainEvents.Count);
            Assert.Contains(buildRoot.DomainEvents, evt => evt is BuildQueuedEvent);

            Mock.VerifyAll();
        }
    }
}
