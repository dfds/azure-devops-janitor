using AzureDevOpsJanitor.Domain.ValueObjects;
using System.Collections.Generic;
using Xunit;

namespace AzureDevOpsJanitor.Domain.UnitTest.ValueObjects
{
    public class BuildDefinitionTests
    {
        [Fact]
        public void CanBeConstructed() 
        {
            //Arrange
            BuildDefinition sut;

            //Act
            sut = new BuildDefinition("my_name", "my_yaml", 0);

            //Assert
            Assert.NotNull(sut);
            Assert.True(sut.Name == "my_name");
            Assert.True(sut.Yaml == "my_yaml");
            Assert.True(sut.DefinitionId == 0);
        }

        [Fact]
        public void GetAtomicValuesYieldsAllProperties()
        {
            //Arrange
            var sut = new BuildDefinition("my_name", "my_yaml", 0);

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
