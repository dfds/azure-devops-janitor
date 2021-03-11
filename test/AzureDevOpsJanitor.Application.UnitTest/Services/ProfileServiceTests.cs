using AutoMapper;
using AzureDevOpsJanitor.Application.Services;
using AzureDevOpsJanitor.Domain.ValueObjects;
using CloudEngineering.CodeOps.Infrastructure.AzureDevOps;
using CloudEngineering.CodeOps.Infrastructure.AzureDevOps.DataTransferObjects.Profile;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Services
{
    public class ProfileServiceTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var mockVstsRestClient = new Mock<IAdoClient>();
            var mockMapper = new Mock<IMapper>();
            var sut = new ProfileService(mockMapper.Object, mockVstsRestClient.Object);

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.Equal(hashCode, sut.GetHashCode());
        }

        [Fact]
        public async Task CanGet()
        {
            //Arrange
            var mockVstsRestClient = new Mock<IAdoClient>();
            var mockMapper = new Mock<IMapper>();
            var profileIdentifier = "me";

            mockVstsRestClient.Setup(m => m.GetProfile(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(new ProfileDto()));
            mockMapper.Setup(m => m.Map<UserProfile>(It.IsAny<ProfileDto>())).Returns(new UserProfile(profileIdentifier));

            var sut = new ProfileService(mockMapper.Object, mockVstsRestClient.Object);

            //Act
            var result = await sut.GetAsync(profileIdentifier);

            //Assert
            Assert.True(result != null);
            Assert.Equal(profileIdentifier, result.Name);

            Mock.VerifyAll();
        }
    }
}
