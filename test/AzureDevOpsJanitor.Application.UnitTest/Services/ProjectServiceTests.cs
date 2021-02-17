using AzureDevOpsJanitor.Application.Services;
using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Repository;
using Moq;
using CloudEngineering.CodeOps.Abstractions.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Services
{
    public class ProjectServiceTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            ProjectService sut;
            var mockProjectRepository = new Mock<IProjectRepository>();

            //Act
            sut = new ProjectService(mockProjectRepository.Object);

            //Assert
            Assert.NotNull(sut);
        }

        [Fact]
        public async Task CanAdd()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockProjectRepository = new Mock<IProjectRepository>();
            var projectName = "abcd";
            var projectRoot = new ProjectRoot(projectName);

            mockUnitOfWork.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mockProjectRepository.SetupGet(m => m.UnitOfWork).Returns(mockUnitOfWork.Object);
            mockProjectRepository.Setup(m => m.Add(It.IsAny<ProjectRoot>())).Returns(projectRoot);

            var sut = new ProjectService(mockProjectRepository.Object);

            //Act
            var result = await sut.AddAsync(projectName);

            //Assert
            Assert.Equal(result, projectRoot);

            Mock.VerifyAll();
        }

        [Fact]
        public async Task CanDelete()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockProjectRepository = new Mock<IProjectRepository>();
            var projectId = Guid.NewGuid();
            var projectName = "abcd";
            var projectRoot = new ProjectRoot(projectName);

            mockUnitOfWork.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mockProjectRepository.SetupGet(m => m.UnitOfWork).Returns(mockUnitOfWork.Object);
            mockProjectRepository.Setup(m => m.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(projectRoot));
            mockProjectRepository.Setup(m => m.Delete(It.IsAny<ProjectRoot>()));

            var sut = new ProjectService(mockProjectRepository.Object);

            //Act
            await sut.DeleteAsync(projectId);

            //Assert
            Mock.VerifyAll();
        }
    }
}
