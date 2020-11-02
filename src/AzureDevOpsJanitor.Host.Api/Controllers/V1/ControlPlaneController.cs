using System;
using System.Dynamic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourceProvisioning.Abstractions.Grid.Provisioning;
using ResourceProvisioning.Broker.Application.Commands.Environment;

namespace ResourceProvisioning.Broker.Host.Api.Controllers.V1
{
	[ApiController]
	[Route("[controller]")]
	public class ControlPlaneController : ControllerBase
	{
		private readonly IProvisioningBroker _broker;
		private readonly IMapper _mapper;

		public ControlPlaneController(IProvisioningBroker broker, IMapper mapper)
		{
			_broker = broker ?? throw new ArgumentNullException(nameof(broker));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery]Guid environmentId = default)
		{
			var cmd = new GetEnvironmentCommand(environmentId);
			var result = await _broker.Handle(cmd);

			if (result?.Content != null)
			{
				return Ok(await result.Content.ReadAsStringAsync());
			}

			return Ok();
		}

		[Authorize(AuthenticationSchemes = AzureADDefaults.JwtBearerAuthenticationScheme)]
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] dynamic payload, [FromQuery] Guid environmentId)
		{
			dynamic requestWrapper = new ExpandoObject();

			requestWrapper.HttpRequest = Request;
			requestWrapper.Payload = payload;

			IProvisioningRequest provisioningRequest = _mapper.Map<IProvisioningRequest>(requestWrapper);

			var result = await _broker.Handle(provisioningRequest);

			return Ok(result);
		}

		[Authorize(AuthenticationSchemes = AzureADDefaults.JwtBearerAuthenticationScheme)]
		[HttpDelete]
		public async Task<IActionResult> Delete([FromQuery] Guid environmentId)
		{
			if (environmentId == Guid.Empty)
			{
				return BadRequest();
			}

			var cmd = new DeleteEnvironmentCommand(environmentId);
			var result = await _broker.Handle(cmd);

			return Ok(await result?.Content?.ReadAsStringAsync());
		}
	}
}
