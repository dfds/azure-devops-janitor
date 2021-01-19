using AzureDevOpsJanitor.Application.Commands.Project;
using AzureDevOpsJanitor.Domain.Aggregates.Project;
using Microsoft.AspNetCore.Mvc;
using ResourceProvisioning.Abstractions.Facade;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.Api.Controllers.V1
{
    [ApiController]
    [Route("[controller]")]
    public sealed class ProjectController : ControllerBase
    {
        private readonly IFacade _applicationFacade;

        public ProjectController(IFacade applicationFacade)
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
