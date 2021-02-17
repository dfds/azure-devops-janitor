using AutoMapper;
using AzureDevOpsJanitor.Application.Commands.Build;
using AzureDevOpsJanitor.Application.Commands.Project;
using AzureDevOpsJanitor.Application.Mapping.Converters;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Project;
using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Commands;
using System;
using Xunit;

namespace AzureDevOpsJanitor.Application.UnitTest.Mapping.Converters
{
    public class AggregateRootToCommandConverterTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            ITypeConverter<IAggregateRoot, ICommand<IAggregateRoot>> sut;

            //Act
            sut = new AggregateRootToCommandConverter();

            //Assert
            Assert.NotNull(sut);
        }

        [Fact]
        public void CanResolveBuildRootWithEmptyIdToCreateBuildCommand()
        {
            //Arrange
            var sut = new AggregateRootToCommandConverter();
            var buildRoot = new BuildRoot(Guid.NewGuid(), "abcd", new Domain.ValueObjects.BuildDefinition("name", "yaml", 1));

            //Act
            var result = sut.Convert(buildRoot);

            //Assert
            Assert.NotNull(result);
            Assert.True(result is CreateBuildCommand);
        }

        [Fact]
        public void CanResolveBuildRootWithIdToUpdateBuildCommand()
        {
            //Arrange
            var sut = new AggregateRootToCommandConverter();
            var buildRoot = new BuildRoot(Guid.NewGuid(), "abcd", new Domain.ValueObjects.BuildDefinition("name", "yaml", 1));

            buildRoot.GetType().GetProperty("Id").SetValue(buildRoot, 1);

            //Act
            var result = sut.Convert(buildRoot);

            //Assert
            Assert.NotNull(result);
            Assert.True(result is UpdateBuildCommand);
        }

        [Fact]
        public void CanResolveProjectRootWithEmptyIdToCreateProjectCommand()
        {
            //Arrange
            var sut = new AggregateRootToCommandConverter();
            var projectRoot = new ProjectRoot("name");

            //Act
            var result = sut.Convert(projectRoot);

            //Assert
            Assert.NotNull(result);
            Assert.True(result is CreateProjectCommand);
        }

        [Fact]
        public void CanResolveProjectRootWithIdToUpdateProjectCommand()
        {
            //Arrange
            var sut = new AggregateRootToCommandConverter();
            var projectRoot = new ProjectRoot("name");

            projectRoot.GetType().GetProperty("Id").SetValue(projectRoot, Guid.NewGuid());

            //Act
            var result = sut.Convert(projectRoot);

            //Assert
            Assert.NotNull(result);
            Assert.True(result is UpdateProjectCommand);
        }
    }
}
