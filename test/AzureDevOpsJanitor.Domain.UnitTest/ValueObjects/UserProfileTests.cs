using AzureDevOpsJanitor.Domain.ValueObjects;
using System.Collections.Generic;
using Xunit;

namespace AzureDevOpsJanitor.Domain.UnitTest.ValueObjects
{
    public class UserProfileTests
    {
        [Fact]
        public void CanBeConstructed() 
        {
            //Arrange
            UserProfile sut;

            //Act
            sut = new UserProfile("my_name");

            //Assert
            Assert.NotNull(sut);
            Assert.True(sut.Name == "my_name");
        }

        [Fact]
        public void GetAtomicValuesYieldsAllProperties()
        {
            //Arrange
            var sut = new UserProfile("my_name");

            //Act
            var type = sut.GetType();
            var properties = type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            var methods = type.GetMethod("GetAtomicValues", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var methodInvokationResult = methods.Invoke(sut, null) as IEnumerable<object>;

            //Assert
            foreach (var prop in properties)
            {
                Assert.Contains(methodInvokationResult, (item) => prop.GetValue(sut).Equals(item));
            }
        }

    }
}
