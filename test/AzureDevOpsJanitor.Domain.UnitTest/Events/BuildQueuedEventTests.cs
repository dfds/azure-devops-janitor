using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Events.Build;
using System;
using Xunit;

namespace AzureDevOpsJanitor.Domain.UnitTest.Events
{
    public class BuildQueuedEventTests
    {
        [Fact]
        public void AreNotEqual() 
        {
            //Arrange
            var buildRoot = new BuildRoot(Guid.Empty, Guid.NewGuid().ToString(), null);
            var sut = new BuildQueuedEvent(buildRoot);

            //Act
            var anotherEvent = new BuildQueuedEvent(buildRoot);

            //Assert
            Assert.True(sut.Build == buildRoot);
            Assert.True(anotherEvent.Build == buildRoot);
            Assert.False(sut.Equals(anotherEvent));
        }
    }
}