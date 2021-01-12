using AzureDevOpsJanitor.Application.Cache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Cache
{
    public class ApplicationCacheTests
    {
        [Fact]
        public void CanBeConstructed() 
        {
            //Arrange
            ApplicationCache sut;
            var mockOptions = new Moq.Mock<IOptions<MemoryCacheOptions>>();

            mockOptions.Setup(opt => opt.Value).Returns(new MemoryCacheOptions());
            
            //Act
            sut = new ApplicationCache(mockOptions.Object);

            //Assert
            Assert.NotNull(sut);
            mockOptions.VerifyAll();
        }

        [Fact]
        public void CanGetOrCreate()
        {
            //Arrange
            var mockOptions = new Moq.Mock<IOptions<MemoryCacheOptions>>();
            mockOptions.Setup(opt => opt.Value).Returns(new MemoryCacheOptions());

            var sut = new ApplicationCache(mockOptions.Object);

            //Act
            sut.GetOrCreate("key", (cacheEntry) => "value");

            //Assert
            Assert.True(sut.Count == 1);
            Assert.NotNull(sut.Get("key"));
            mockOptions.VerifyAll();
        }

        [Fact]
        public async Task CanSetAndExpire()
        {
            //Arrange
            var expirationInMilliseconds = 1;
            var mockOptions = new Moq.Mock<IOptions<MemoryCacheOptions>>();
            mockOptions.Setup(opt => opt.Value).Returns(new MemoryCacheOptions() { ExpirationScanFrequency = TimeSpan.FromMilliseconds(expirationInMilliseconds) });

            var sut = new ApplicationCache(mockOptions.Object);

            //Act
            sut.Set("key", "value", TimeSpan.FromMilliseconds(expirationInMilliseconds));

            await Task.Delay(expirationInMilliseconds);

            //Assert
            Assert.Null(sut.Get("key"));
            mockOptions.VerifyAll();
        }

        [Fact]
        public void CanRemove()
        {
            //Arrange
            var mockOptions = new Moq.Mock<IOptions<MemoryCacheOptions>>();
            mockOptions.Setup(opt => opt.Value).Returns(new MemoryCacheOptions());

            var sut = new ApplicationCache(mockOptions.Object);

            //Act
            sut.Set("key", "value");
            sut.Remove("key");

            //Assert
            Assert.True(sut.Count == 0);
            Assert.Null(sut.Get("key"));
            mockOptions.VerifyAll();
        }
    }
}
