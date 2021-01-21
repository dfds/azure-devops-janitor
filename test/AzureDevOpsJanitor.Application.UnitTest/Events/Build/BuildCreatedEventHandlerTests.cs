using AutoMapper;
using AzureDevOpsJanitor.Application.Events.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.ValueObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using Moq;
using System;
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
            var mockVstsRestClient = new Mock<IVstsRestClient>();
            var sut = new BuildCreatedEventHandler(mockMapper.Object, mockVstsRestClient.Object);

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
            var mockVstsRestClient = new Mock<IVstsRestClient>();
            var fakeVstsPayload = new DefinitionReferenceDto()
            {
                Id = 1,
                Name = "my-def",
                QueueStatus = "my-queue-status",
                Type = "my-type",
                Revision = 1
            };

            mockMapper.Setup(m => m.Map<DefinitionReferenceDto>(It.IsAny<BuildDefinition>())).Returns(fakeVstsPayload);
            mockVstsRestClient.Setup(m => m.CreateDefinition(It.IsAny<string>(), It.IsAny<string>(), fakeVstsPayload));

            var sut = new BuildCreatedEventHandler(mockMapper.Object, mockVstsRestClient.Object);

            //Act
            await sut.Handle(new Domain.Events.Build.BuildCreatedEvent(new BuildRoot(Guid.NewGuid(), "my-capability-identifier", new BuildDefinition("name", "yaml", 1))));

            //Assert
            Mock.VerifyAll();
        }
    }
}
