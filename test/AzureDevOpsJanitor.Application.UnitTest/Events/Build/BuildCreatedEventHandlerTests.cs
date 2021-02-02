using AutoMapper;
using AzureDevOpsJanitor.Application.Events.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Events.Build;
using AzureDevOpsJanitor.Domain.Services;
using AzureDevOpsJanitor.Domain.ValueObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Events.Build
{
    public class BuildCreatedEventHandlerTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var mockMapper = new Mock<IMapper>();
            var mockVstsRestClient = new Mock<IVstsClient>();
            var mockProjectService = new Mock<IProjectService>();
            var sut = new BuildCreatedEventHandler(mockMapper.Object, mockVstsRestClient.Object, mockProjectService.Object);

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetHashCode());

            Mock.VerifyAll();
        }

        [Fact]
        public async Task CanHandleEvent()
        {
            //Arrange
            var mockMapper = new Mock<IMapper>();
            var mockVstsRestClient = new Mock<IVstsClient>();
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

            mockMapper.Setup(m => m.Map<BuildDefinitionDto>(It.IsAny<BuildDefinition>())).Returns(fakeVstsPayload);
            mockVstsRestClient.Setup(m => m.CreateBuildDefinition(fakeProjectPayload.Name, fakeVstsPayload, It.IsAny<string>(), It.IsAny<CancellationToken>()));
            mockProjectService.Setup(m => m.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(fakeProjectPayload));

            var sut = new BuildCreatedEventHandler(mockMapper.Object, mockVstsRestClient.Object, mockProjectService.Object);

            //Act
            await sut.Handle(new BuildCreatedEvent(new BuildRoot(fakeProjectPayload.Id, "my-capability-identifier", new BuildDefinition("name", "yaml", 1))));

            //Assert
            Mock.VerifyAll();
        }
    }
}
