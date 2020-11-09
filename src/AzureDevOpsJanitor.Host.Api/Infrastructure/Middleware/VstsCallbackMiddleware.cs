using AzureDevOpsJanitor.Host.Api.Models.Vsts;
using AzureDevOpsJanitor.Host.Api.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureDevOpsJanitor.Host.Api.Infrastructure.Middleware
{
    //TODO: Remove middleware once a proper client authentication flow has been established. This will only work in local dev.
    public class VstsCallbackMiddleware : IMiddleware
    {
        public const string VstsAccessTokenCacheKey = "vstsAccessToken";
        private readonly IMemoryCache _cache;
        private readonly VstsSettings _vstsSettings;

        public VstsCallbackMiddleware(IMemoryCache cache, IConfiguration config)
        {
            _cache = cache;
            _vstsSettings = config.GetSection("Vsts").Get<VstsSettings>();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path.Value == _vstsSettings.RedirectUri.AbsolutePath)
            {
                if (!context.Request.QueryString.HasValue || !context.Request.QueryString.Value.Contains("code")) {
                    throw new Exception("Missing code query param in oauth2 callback");
                }

                var formData = new Dictionary<string, string>();
                var code = QueryHelpers.ParseQuery(context.Request.QueryString.Value)["code"];

                using var client = new HttpClient();

                formData.Add("client_assertion", _vstsSettings.ClientSecret);
                formData.Add("client_assertion_type", "urn:ietf:params:oauth:client-assertion-type:jwt-bearer");
                formData.Add("assertion", code);
                formData.Add("grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer");
                formData.Add("redirect_uri", _vstsSettings.RedirectUri.AbsoluteUri);

                var stsResponse = await client.PostAsync(_vstsSettings.TokenService.AbsoluteUri, new FormUrlEncodedContent(formData));
                var stsResponseData = await stsResponse.Content.ReadAsStringAsync();
                var stsPayload = JsonSerializer.Deserialize<StsPayload>(stsResponseData);
                var tokenHandler = new JwtSecurityTokenHandler();

                if (tokenHandler.CanReadToken(stsPayload.AccessToken))
                {
                    var token = tokenHandler.ReadJwtToken(stsPayload.AccessToken);

                    if (double.TryParse(stsPayload.ExpiresIn, out double expires))
                    {
                        _cache.Set(VstsAccessTokenCacheKey, token.RawData, TimeSpan.FromSeconds(expires));
                    }
                }
            }

            await next(context);
        }
    }
}
