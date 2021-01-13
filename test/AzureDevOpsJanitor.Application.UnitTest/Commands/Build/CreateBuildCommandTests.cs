using AzureDevOpsJanitor.Application.Commands.Build;
using AzureDevOpsJanitor.Domain.ValueObjects;
using System;
using System.Text.Json;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Commands.Build
{
    public class CreateBuildCommandTests
    {
        [Fact]
        public void CanBeConstructed() 
        {
            //Arrange
            CreateBuildCommand sut;
            
            //Act
            sut = new CreateBuildCommand(Guid.NewGuid(), Guid.NewGuid().ToString(), new BuildDefinition("my-def", "my-yaml", 1));

            //Assert
            Assert.NotNull(sut);
            Assert.True(sut.ProjectId != Guid.Empty);
            Assert.True(!string.IsNullOrEmpty(sut.CapabilityId));
            Assert.NotNull(sut.BuildDefinition);
            Assert.Equal(1, sut.BuildDefinition.Id);
            Assert.Equal("my-def", sut.BuildDefinition.Name);
            Assert.Equal("my-yaml", sut.BuildDefinition.Yaml);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new CreateBuildCommand(Guid.NewGuid(), Guid.NewGuid().ToString(), new BuildDefinition("my-def", "my-yaml", 1));

            //Act
            var json = JsonSerializer.Serialize(sut);

            //Assert
            Assert.False(string.IsNullOrEmpty(json));
        }

        [Fact]
        public void CanBeDeserialized()
        {
            //Arrange
            CreateBuildCommand sut;
            var json = "{\"projectId\":\"6def4aee-1467-4613-9b1b-5c4bf4cbbe89\",\"buildDefinition\":{\"id\":1,\"name\":\"my-def\",\"yaml\":\"my-yaml\"},\"capabilityId\":\"12d44c07-8950-4eb9-9398-6a815c35a08c\"}";
            
            //Act
            sut = JsonSerializer.Deserialize<CreateBuildCommand>(json);

            //Assert
            Assert.NotNull(sut);
            Assert.Equal("6def4aee-1467-4613-9b1b-5c4bf4cbbe89", sut.ProjectId.ToString());
            Assert.Equal("12d44c07-8950-4eb9-9398-6a815c35a08c", sut.CapabilityId);
            Assert.NotNull(sut.BuildDefinition);
            Assert.Equal(1, sut.BuildDefinition.Id);
            Assert.Equal("my-def", sut.BuildDefinition.Name);
            Assert.Equal("my-yaml", sut.BuildDefinition.Yaml);
        }
    }
}
