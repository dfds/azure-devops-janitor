using AzureDevOpsJanitor.Application.Services;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Events.Build;
using AzureDevOpsJanitor.Domain.Repository;
using AzureDevOpsJanitor.Domain.ValueObjects;
using Moq;
using ResourceProvisioning.Abstractions.Data;
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
        }

        [Fact]
        public async Task CanAdd()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBuildRepository = new Mock<IBuildRepository>();
            var sut = new BuildService(mockBuildRepository.Object);
            var projectId = Guid.NewGuid();
            var buildRoot = new BuildRoot(projectId, "my-capability-identifier-or-guid", new BuildDefinition("name", "yaml", 1));

            mockUnitOfWork.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mockBuildRepository.SetupGet(m => m.UnitOfWork).Returns(mockUnitOfWork.Object);
            mockBuildRepository.Setup(m => m.Add(It.IsAny<BuildRoot>())).Returns(buildRoot);

            //Act
            var result = await sut.AddAsync(projectId, "my-capability-identifier-or-guid", new BuildDefinition("name", "yaml", 1));

            //Assert
            Assert.Equal(result, buildRoot);

            mockBuildRepository.VerifyAll();
        }

        [Fact]
        public async Task CanDelete()
        {
            //Arrange
            var buildId = 1;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBuildRepository = new Mock<IBuildRepository>();
            var mockBuildDefinition = new BuildDefinition("name", "yaml", buildId);
            var mockBuildRoot = new BuildRoot(Guid.NewGuid(), "my-capability-identifier-or-guid", mockBuildDefinition);

            mockUnitOfWork.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mockBuildRepository.SetupGet(m => m.UnitOfWork).Returns(mockUnitOfWork.Object);
            mockBuildRepository.Setup(m => m.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(mockBuildRoot));
            mockBuildRepository.Setup(m => m.Delete(It.IsAny<BuildRoot>()));

            var sut = new BuildService(mockBuildRepository.Object);

            //Act
            await sut.DeleteAsync(buildId);

            //Assert
            mockBuildRepository.VerifyAll();
        }

        [Fact]
        public async Task CanQueue()
        {
            //Arrange
            var buildId = 1;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBuildRepository = new Mock<IBuildRepository>();
            var mockBuildDefinition = new BuildDefinition("name", "yaml", buildId);
            var mockBuildRoot = new BuildRoot(Guid.NewGuid(), "my-capability-identifier-or-guid", mockBuildDefinition);

            mockUnitOfWork.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mockBuildRepository.SetupGet(m => m.UnitOfWork).Returns(mockUnitOfWork.Object);
            mockBuildRepository.Setup(m => m.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(mockBuildRoot));

            var sut = new BuildService(mockBuildRepository.Object);
            
            //Act
            await sut.QueueAsync(buildId);

            //Assert
            Assert.Equal(2, mockBuildRoot.DomainEvents.Count);
            Assert.Contains(mockBuildRoot.DomainEvents, evt => evt is BuildQueuedEvent);

            mockBuildRepository.VerifyAll();
        }
    }
}
