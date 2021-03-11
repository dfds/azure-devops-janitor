using AutoMapper;
using AzureDevOpsJanitor.Application.Events.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Services;
using AzureDevOpsJanitor.Domain.ValueObjects;
using CloudEngineering.CodeOps.Infrastructure.AzureDevOps;
using CloudEngineering.CodeOps.Infrastructure.AzureDevOps.DataTransferObjects.Build;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Events.Build
{
    public class BuildQueuedEventHandlerTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var mockMapper = new Mock<IMapper>();
            var mockVstsRestClient = new Mock<IAdoClient>();
            var mockProjectService = new Mock<IProjectService>();
            var sut = new BuildQueuedEventHandler(mockMapper.Object, mockVstsRestClient.Object, mockProjectService.Object);

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetHashCode());
        }

        [Fact]
        public async Task CanHandleEvent()
        {
            //Arrange
            var mockMapper = new Mock<IMapper>();
            var mockVstsRestClient = new Mock<IAdoClient>();
            var mockProjectService = new Mock<IProjectService>();
            var fakeVstsPayload = new BuildDefinitionDto()
            {
                Id = 1,
                Name = "my-def",
                QueueStatus = "my-queue-status",
                Type = "my-type",
                Revision = 1
            };
            var fakeProjectPayload = new ProjectRoot("foo");

            var sut = new BuildQueuedEventHandler(mockMapper.Object, mockVstsRestClient.Object, mockProjectService.Object);

            mockMapper.Setup(m => m.Map<BuildDefinitionDto>(It.IsAny<BuildDefinition>())).Returns(fakeVstsPayload);
            mockVstsRestClient.Setup(m => m.CreateBuildDefinition(fakeProjectPayload.Name, fakeVstsPayload, It.IsAny<string>(), It.IsAny<CancellationToken>()));
            mockProjectService.Setup(m => m.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(fakeProjectPayload));

            //Act
            await sut.Handle(new Domain.Events.Build.BuildQueuedEvent(new Domain.Aggregates.Build.BuildRoot(Guid.NewGuid(), "my-capability-identifier", new BuildDefinition("name", "yaml", 1))));

            //Assert
            Mock.VerifyAll();
        }
    }
}
