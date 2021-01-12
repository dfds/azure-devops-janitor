using AzureDevOpsJanitor.Domain.Aggregates.Build;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AzureDevOpsJanitor.Domain.UnitTest.Events.Build
{
    public class BuildStatusTests
    {
        [Fact]
        public void VerifyEnumerations()
        {
            //Arrange
            IEnumerable<BuildStatus> sut;

            //Act
            sut = ResourceProvisioning.Abstractions.Entities.EntityEnumeration.GetAll<BuildStatus>();

            //Assert
            Assert.True(sut.Count() == 6);
            Assert.Contains(sut, (item) => item.Id == 1 && item.Name == "created");
            Assert.Contains(sut, (item) => item.Id == 2 && item.Name == "queued");
            Assert.Contains(sut, (item) => item.Id == 4 && item.Name == "succeeded");
            Assert.Contains(sut, (item) => item.Id == 8 && item.Name == "failed");
            Assert.Contains(sut, (item) => item.Id == 16 && item.Name == "stopped");
            Assert.Contains(sut, (item) => item.Id == 32 && item.Name == "partial");
        }
    }
}