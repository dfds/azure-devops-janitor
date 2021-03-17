using AzureDevOpsJanitor.Application;
using AzureDevOpsJanitor.Application.Commands.Profile;
using AzureDevOpsJanitor.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.Api.Controllers.V1
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("dfds.all.read")]
    public sealed class ProfileController : ControllerBase
    {
        private readonly IApplicationFacade _applicationFacade;

        public ProfileController(IApplicationFacade applicationFacade)
        {
            _applicationFacade = applicationFacade ?? throw new ArgumentNullException(nameof(applicationFacade));
        }

        [HttpGet]
        public async Task<UserProfile> Get([FromQuery] string profileId = null)
        {
            return await _applicationFacade.Execute(new GetProfileCommand(profileId));
        }
    }
}
