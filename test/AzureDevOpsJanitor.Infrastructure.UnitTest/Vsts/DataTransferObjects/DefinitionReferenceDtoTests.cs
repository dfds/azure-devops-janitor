﻿using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using System;
using System.Text.Json;
using Xunit;

namespace AzureDevOpsJanitor.Infrastructure.UnitTest.Vsts.Events
{
    public class DefinitionReferenceDtoTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            DefinitionReferenceDto sut;

            //Act
            sut = new DefinitionReferenceDto();

            //Assert
            Assert.NotNull(sut);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new DefinitionReferenceDto() { 
                Id = 1,
                Name = "MyName",
                Project = "MyProject",
                QueueStatus = "MyQueueStatus",
                Revision = "MyRevision",
                Type = "MyType",
                Uri = new Uri("https://foo.bar")
            };
            
            //Act
            var payload = JsonSerializer.Serialize(sut, new JsonSerializerOptions { IgnoreNullValues = true });

            //Assert
            Assert.NotNull(JsonDocument.Parse(payload));
        }

        [Fact]
        public void CanBeDeserialized()
        {
            //Arrange
            DefinitionReferenceDto sut;

            //Act
            sut = JsonSerializer.Deserialize<DefinitionReferenceDto>("{\"id\":1,\"name\":\"MyName\",\"project\":\"MyProject\",\"revision\":\"MyRevision\",\"type\":\"MyType\",\"uri\":\"https://foo.bar\",\"queueStatus\":\"MyQueueStatus\"}");

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(1, sut.Id);
            Assert.Equal("MyName", sut.Name);
            Assert.Equal("MyProject", sut.Project);
            Assert.Equal("MyQueueStatus", sut.QueueStatus);
            Assert.Equal("MyRevision", sut.Revision);
            Assert.Equal("MyType", sut.Type);
            Assert.Equal("https://foo.bar", sut.Uri.AbsoluteUri);
        }
    }
}