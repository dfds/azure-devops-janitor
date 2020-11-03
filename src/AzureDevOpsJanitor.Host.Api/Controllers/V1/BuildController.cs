using AutoMapper;
using AzureDevOpsJanitor.Domain.Services;
using AzureDevOpsJanitor.Host.Api.Models.Ado;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.Api.Controllers.V1
{
	[ApiController]
	[Route("[controller]")]
	public class BuildController : ControllerBase
	{
		private readonly IBuildService _buildService;
		private readonly IMapper _mapper;

		public BuildController(IBuildService buildService, IMapper mapper)
		{
			_buildService = buildService ?? throw new ArgumentNullException(nameof(buildService));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

        //Browser authorize code link (ado sts): https://app.vssps.visualstudio.com/oauth2/authorize?client_id=B6C0CFEB-7BD5-4A1A-A67B-87F5EC9788CA&response_type=Assertion&state=123456&scope=vso.analytics%20vso.build_execute%20vso.code_full%20vso.code_status%20vso.dashboards_manage%20vso.entitlements%20vso.extension%20vso.extension.data%20vso.gallery%20vso.graph_manage%20vso.identity_manage%20vso.loadtest%20vso.machinegroup_manage%20vso.notification_diagnostics%20vso.notification_manage%20vso.packaging%20vso.profile_write%20vso.project_manage%20vso.release_manage%20vso.serviceendpoint_manage%20vso.taskgroups_manage%20vso.test%20vso.threads_full%20vso.work_full&redirect_uri=https%3A%2F%2Flocalhost%3A54322%2Fadoproxy%2Fcallback%2Foauth2%2Fvsts

        [HttpGet]
        [Route("callback/oauth2/vsts")]
        public async Task Callback([FromQuery] string code, [FromQuery] string state)
        {
            var client = new HttpClient();
            var kvps = new Dictionary<string, string>();

            kvps.Add("grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer");
            kvps.Add("client_assertion_type", "urn:ietf:params:oauth:client-assertion-type:jwt-bearer");
            kvps.Add("client_assertion", "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Im9PdmN6NU1fN3AtSGpJS2xGWHo5M3VfVjBabyJ9.eyJjaWQiOiJiNmMwY2ZlYi03YmQ1LTRhMWEtYTY3Yi04N2Y1ZWM5Nzg4Y2EiLCJjc2kiOiI0YWFhY2E0ZC1jNDI5LTRiYjQtODhkOC1kMGQ0ZGQ4ZTI5OGEiLCJuYW1laWQiOiJjNDFmZDZiMi1lMmZmLTYxNDQtOGU0My02MWE2ODNmYjJhZjEiLCJpc3MiOiJhcHAudnN0b2tlbi52aXN1YWxzdHVkaW8uY29tIiwiYXVkIjoiYXBwLnZzdG9rZW4udmlzdWFsc3R1ZGlvLmNvbSIsIm5iZiI6MTYwNDA1MDY0NCwiZXhwIjoxNzYxODE3MDQ0fQ.Y1UQhZQIq9XquWzqkWO4VxYXWgnkTBf3dKrW7JEituyJLETOFdH2l5AuhwurMRC3AVlojs6uxTylSJMLdjv_0Ijnfg8we1ao_YHUCPGRWWWLSP2PQri7VCix5LxU961B-eN0S5eY0eOoTifl3KbGkcOK7oOidPPxsD-y1qdP0EE8Tj1FkxdNVA77KWKjyTdDRcr4ixyG68DvHWmyv2vxx4OBnUhqg4MarU7O7ej0BGB7w-JrNIlrtk9jrELQHBftwxNEXCpugwS0KUuGXZrJ1-aioapaZ5rKMMvxYlT0yAAI66wNe15n1EUQlJ2AzU-OgLDl4AizjNq7DOaukZpMuw");
            kvps.Add("assertion", code);
            kvps.Add("redirect_uri", "https://localhost:54322/adoproxy/callback/oauth2/vsts");

            var stsPayload = new FormUrlEncodedContent(kvps);
            var stsResponse = await client.PostAsync("https://app.vssps.visualstudio.com/oauth2/token", stsPayload);
            var stsData = JsonSerializer.Deserialize<StsPayload>(await stsResponse.Content.ReadAsStringAsync());
            var accessToken = stsData.AccessToken;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var clock = System.Diagnostics.Stopwatch.StartNew();

            var adoProfileResponse = await client.GetAsync("https://app.vssps.visualstudio.com/_apis/profile/profiles/me?api-version=6.1-preview.3");
            var adoProfileData = await adoProfileResponse.Content.ReadAsStringAsync();
            var adoProfile = JsonSerializer.Deserialize<Models.Ado.Profile>(adoProfileData);

            var profileCall = clock.ElapsedMilliseconds;

            var adoProjectsResponse = await client.GetAsync("https://dev.azure.com/dfds/_apis/projects?api-version=6.0");
            var adoProjectsData = await adoProjectsResponse.Content.ReadAsStringAsync();
            var adoProjects = JsonSerializer.Deserialize<ProjectCollection>(adoProjectsData);

            var projectsCall = clock.ElapsedMilliseconds;

            var allBuilds = new List<Build>();

            foreach (var project in adoProjects.Items)
            {
                var adoProjectBuildsResponse = await client.GetAsync($"https://dev.azure.com/dfds/{project.Id}/_apis/build/builds?api-version=6.1-preview.6&requestedFor={adoProfile.Id}");
                var adoProjectBuildsData = await adoProjectBuildsResponse.Content.ReadAsStringAsync();
                var adoProjectBuilds = JsonSerializer.Deserialize<BuildCollection>(adoProjectBuildsData);

                if (adoProjectBuilds.Items.Count() > 0)
                {
                    allBuilds.AddRange(adoProjectBuilds.Items);
                }
            }

            var x = clock.ElapsedMilliseconds;
            clock.Stop();

            var y = "";
        }

        [HttpPost]
        [Route("callback/hooks/ado/build/completed")]
        public async Task Webhook([FromBody] JsonElement @event)
        {
            var y = @event;
        }
    }
}
