using AzureDevOpsJanitor.Domain.Aggregates.Build;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace AzureDevOpsJanitor.Domain.UnitTest.Events.Build
{
    public class BuildRootTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            BuildRoot sut;

            //Act
            sut = new BuildRoot(Guid.NewGuid(), Guid.NewGuid().ToString(), null);

            //Assert
            Assert.NotNull(sut);
            Assert.True(sut.DomainEvents.Count == 1);
            Assert.True(sut.Status == BuildStatus.Created);
        }

        [Fact]
        public void CanDetectValidConstruction()
        {
            //Arrange
            var sut = new BuildRoot(Guid.NewGuid(), Guid.NewGuid().ToString(), null);
            var validationCtx = new ValidationContext(this);

            //Act
            var validationResults = sut.Validate(validationCtx);

            //Assert
            Assert.True(!validationResults.Any());
        }

        [Fact]
        public void CanDetectInvalidConstruction()
        {
            //Arrange
            var sut = new BuildRoot(Guid.Empty, string.Empty, null);
            var validationCtx = new ValidationContext(this);

            //Act
            var validationResults = sut.Validate(validationCtx);

            //Assert
            Assert.True(validationResults.Count() == 2);
        }

        [Fact]
        public void CanTransitionFromCreatedToQueued()
        {
            //Arrange
            var sut = new BuildRoot(Guid.NewGuid(), Guid.NewGuid().ToString(), null);

            //Act
            sut.Queue();

            //Assert
            Assert.True(sut.DomainEvents.Count == 2);
            Assert.True(sut.Status == BuildStatus.Queued);
        }

        [Fact]
        public void CanTransitionFromQueuedToCompleted()
        {
            //Arrange
            var sut = new BuildRoot(Guid.NewGuid(), Guid.NewGuid().ToString(), null);

            //Act
            sut.Queue();
            sut.Failed();


            //Assert
            Assert.True(sut.DomainEvents.Count == 3);
            Assert.True(sut.Status == BuildStatus.Failed);
        }

        [Fact]
        public void CannotTransitionFromCreatedToCompleted()
        {
            //Arrange
            var sut = new BuildRoot(Guid.NewGuid(), Guid.NewGuid().ToString(), null);

            //Act
            sut.Failed();

            //Assert
            Assert.True(sut.DomainEvents.Count == 1);
            Assert.True(sut.Status == BuildStatus.Created);
        }
    }
}