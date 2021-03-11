using AzureDevOpsJanitor.Application.Commands.Build;
using AzureDevOpsJanitor.Domain.Aggregates.Build;
using AzureDevOpsJanitor.Host.Api.Models;
using CloudEngineering.CodeOps.Abstractions.Facade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.Api.Controllers.V1
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("dfds.all.execute")]
    public sealed class BuildController : ControllerBase
    {
        private readonly IFacade _applicationFacade;

        public BuildController(IFacade applicationFacade)
        {
            _applicationFacade = applicationFacade ?? throw new ArgumentNullException(nameof(applicationFacade));
        }

        [HttpGet]
        public async Task<IEnumerable<BuildRoot>> Get()
        {
            return await _applicationFacade.Execute(new GetBuildCommand());
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<BuildRoot>> Get(int buildId)
        {
            return await _applicationFacade.Execute(new GetBuildCommand(buildId));
        }

        [HttpGet("{projectId}")]
        public async Task<IEnumerable<BuildRoot>> Get(Guid projectId)
        {
            return await _applicationFacade.Execute(new GetBuildCommand(projectId: projectId));
        }

        [HttpPost]
        public async Task<BuildRoot> Post(CreateBuildModel model)
        {
            return await _applicationFacade.Execute(new CreateBuildCommand(model.ProjectId, model.CapabilityIdentifier, model.Definition));
        }

        [HttpDelete]
        public async Task<bool> Delete(int buildId)
        {
            return await _applicationFacade.Execute(new DeleteBuildCommand(buildId));
        }
    }
}
