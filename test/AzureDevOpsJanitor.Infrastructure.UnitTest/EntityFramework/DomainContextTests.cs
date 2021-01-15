using AzureDevOpsJanitor.Domain.Aggregates.Project;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using ResourceProvisioning.Abstractions.Events;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AzureDevOpsJanitor.Infrastructure.UnitTest.EntityFramework
{
    public class DomainContextTests : IClassFixture<DomainContextFixture>
    {
        private readonly DomainContextFixture _fixture;

        public DomainContextTests(DomainContextFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var sut = _fixture.GetDbContext();

            //Act
            var hashCode = sut.GetType().GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetType().GetHashCode());
        }

        [Fact]
        public async Task CanPublishDomainEventsOnSaveEntities()
        {
            //Arrange
            var entityToAdd = new ProjectRoot("my-project");
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Publish(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()));
                        
            var sut = _fixture.GetDbContext(mockMediator.Object);

            //Act
            sut.Database.Migrate();

            var attachedEntity = await sut.AddAsync(entityToAdd);
            var result = await sut.SaveEntitiesAsync(new CancellationToken());

            //Assert
            Assert.NotNull(sut);
            Assert.True(result);
            Assert.True(attachedEntity.Entity.Id != Guid.Empty);

            Mock.VerifyAll();
        }
    }
}
