using AzureDevOpsJanitor.Domain.Aggregates.Project;
using AzureDevOpsJanitor.Domain.Events.Project;
using Xunit;

namespace AzureDevOpsJanitor.Domain.UnitTest.Events.Project
{
    public class ProjectCreatedEventTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            ProjectCreatedEvent sut;

            //Act
            sut = new ProjectCreatedEvent(null);

            //Assert
            Assert.NotNull(sut);
            Assert.True(sut.Project == null);
        }

        [Fact]
        public void AreNotEqual() 
        {
            //Arrange
            var projectRoot = new ProjectRoot(string.Empty);
            var sut = new ProjectCreatedEvent(projectRoot);

            //Act
            var anotherEvent = new ProjectCreatedEvent(projectRoot);

            //Assert
            Assert.True(sut.Project == projectRoot);
            Assert.True(anotherEvent.Project == projectRoot);
            Assert.False(sut.Equals(anotherEvent));
        }
    }
}