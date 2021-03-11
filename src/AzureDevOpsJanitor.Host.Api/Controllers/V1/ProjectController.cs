using AzureDevOpsJanitor.Application;
using AzureDevOpsJanitor.Application.Commands.Project;
using AzureDevOpsJanitor.Domain.Aggregates.Project;
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
    [Authorize("dfds.all.read")]
    public sealed class ProjectController : ControllerBase
    {
        private readonly IApplicationFacade _applicationFacade;

        public ProjectController(IApplicationFacade applicationFacade)
        {
            _applicationFacade = applicationFacade ?? throw new ArgumentNullException(nameof(applicationFacade));
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectRoot>> Get()
        {
            return await _applicationFacade.Execute(new GetProjectCommand());
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<ProjectRoot>> Get(Guid projectId)
        {
            return await _applicationFacade.Execute(new GetProjectCommand(projectId));
        }
    }
}
