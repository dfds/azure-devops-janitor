using AutoMapper;
using AzureDevOpsJanitor.Application.Events.Build;
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
    public class BuildQueuedEventHandlerTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var mockMapper = new Mock<IMapper>();
            var mockVstsRestClient = new Mock<IVstsClient>();
            var sut = new BuildQueuedEventHandler(mockMapper.Object, mockVstsRestClient.Object);

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
            var mockVstsRestClient = new Mock<IVstsClient>();
            var fakeVstsPayload = new DefinitionDto()
            {
                Id = 1,
                Name = "my-def",
                QueueStatus = "my-queue-status",
                Type = "my-type",
                Revision = 1
            };

            var sut = new BuildQueuedEventHandler(mockMapper.Object, mockVstsRestClient.Object);

            mockMapper.Setup(m => m.Map<DefinitionDto>(It.IsAny<BuildDefinition>())).Returns(fakeVstsPayload);
            mockVstsRestClient.Setup(m => m.QueueBuild(It.IsAny<string>(), It.IsAny<string>(), fakeVstsPayload, It.IsAny<CancellationToken>()));

            //Act
            await sut.Handle(new Domain.Events.Build.BuildQueuedEvent(new Domain.Aggregates.Build.BuildRoot(Guid.NewGuid(), "my-capability-identifier", new BuildDefinition("name", "yaml", 1))));

            //Assert
            Mock.VerifyAll();
        }
    }
}
