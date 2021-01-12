using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Events.Build;
using System;
using Xunit;

namespace AzureDevOpsJanitor.Domain.UnitTest.Events.Build
{
    public class BuildCompletedEventTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            BuildCompletedEvent sut;

            //Act
            sut = new BuildCompletedEvent(null);

            //Assert
            Assert.NotNull(sut);
            Assert.True(sut.Build == null);
        }

        [Fact]
        public void AreNotEqual() 
        {
            //Arrange
            var buildRoot = new BuildRoot(Guid.Empty, Guid.NewGuid().ToString(), null);
            var sut = new BuildCompletedEvent(buildRoot);

            //Act
            var anotherEvent = new BuildCompletedEvent(buildRoot);

            //Assert
            Assert.True(sut.Build == buildRoot);
            Assert.True(anotherEvent.Build == buildRoot);
            Assert.False(sut.Equals(anotherEvent));
        }
    }
}