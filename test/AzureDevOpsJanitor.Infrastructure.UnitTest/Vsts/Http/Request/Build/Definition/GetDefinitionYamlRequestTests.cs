﻿using CloudEngineering.CodeOps.Infrastructure.AzureDevOps.Http.Request.Build.Definition;
using System.Net.Http;
using Xunit;

namespace CloudEngineering.CodeOps.Infrastructure.UnitTest.Vsts.Http.Request.Build.Definition
{
    public class GetDefinitionYamlRequestTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            GetBuildDefinitionYamlRequest sut;

            //Act
            sut = new GetBuildDefinitionYamlRequest("my-org", "my-project", 1);

            //Assert
            Assert.NotNull(sut);
            Assert.Equal("6.1-preview.7", sut.ApiVersion);
            Assert.Equal(HttpMethod.Get, sut.Method);
            Assert.Equal("https://dev.azure.com/my-org/my-project/_apis/build/definitions/1/yaml?api-version=6.1-preview.7", sut.RequestUri.AbsoluteUri);
        }
    }
}