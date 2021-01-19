using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Events.Build;
using System;
using Xunit;

namespace AzureDevOpsJanitor.Domain.UnitTest.Events.Build
{
    public class BuildCreatedEventTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            BuildCreatedEvent sut;

            //Act
            sut = new BuildCreatedEvent(null);

            //Assert
            Assert.NotNull(sut);
            Assert.True(sut.Build == null);
        }

        [Fact]
        public void AreNotEqual()
        {
            //Arrange
            var buildRoot = new BuildRoot(Guid.Empty, Guid.NewGuid().ToString(), null);
            var sut = new BuildCreatedEvent(buildRoot);

            //Act
            var anotherEvent = new BuildCreatedEvent(buildRoot);

            //Assert
            Assert.True(sut.Build == buildRoot);
            Assert.True(anotherEvent.Build == buildRoot);
            Assert.False(sut.Equals(anotherEvent));
        }
    }
}