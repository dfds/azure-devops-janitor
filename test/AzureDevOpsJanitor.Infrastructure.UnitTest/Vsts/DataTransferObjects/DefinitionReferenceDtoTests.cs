using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
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
            DefinitionDto sut;

            //Act
            sut = new DefinitionDto();

            //Assert
            Assert.NotNull(sut);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new DefinitionDto()
            {
                Id = 1,
                Name = "MyName",
                QueueStatus = "MyQueueStatus",
                Revision = 1,
                Type = "MyType",
                Uri = new Uri("https://foo.bar/")
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
            DefinitionDto sut;

            //Act
            sut = JsonSerializer.Deserialize<DefinitionDto>("{\"id\":1,\"name\":\"MyName\",\"revision\":1,\"type\":\"MyType\",\"uri\":\"https://foo.bar\",\"queueStatus\":\"MyQueueStatus\"}");

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(1, sut.Id);
            Assert.Equal("MyName", sut.Name);
            Assert.Equal("MyQueueStatus", sut.QueueStatus);
            Assert.Equal(1, sut.Revision);
            Assert.Equal("MyType", sut.Type);
            Assert.Equal("https://foo.bar/", sut.Uri.AbsoluteUri);
        }
    }
}