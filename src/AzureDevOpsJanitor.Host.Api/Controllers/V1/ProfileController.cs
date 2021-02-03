using AzureDevOpsJanitor.Application.Commands.Profile;
using AzureDevOpsJanitor.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourceProvisioning.Abstractions.Facade;
using System;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.Api.Controllers.V1
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public sealed class ProfileController : ControllerBase
    {
        private readonly IFacade _applicationFacade;

        public ProfileController(IFacade applicationFacade)
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
