using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AzureDevOpsJanitor.Host.EventForwarder.Enablers.ApiKey;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AzureDevOpsJanitor.Host.EventForwarder.Filters
{
    public class ApiKeyAuthorizeAsyncFilter : IAsyncAuthorizationFilter
    {
        //TODO: Replace static with const. Unless its intended behavior that people should be able to change this dynamically? (generally a bad idea)
        public static string ApiKeyHeaderName = "x-apiKey";
        public static string ClientIdHeaderName = "x-clientId";
 
        private readonly ILogger<ApiKeyAuthorizeAsyncFilter> _logger;
        private readonly IApiKeyService _apiKeyService;
 
        public ApiKeyAuthorizeAsyncFilter(ILogger<ApiKeyAuthorizeAsyncFilter> logger, IApiKeyService apiKeyService)
        {
            _logger = logger;
            _apiKeyService = apiKeyService;
        }
 
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var request = context.HttpContext.Request;
            var hasApiKeyHeader = request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyValue);
 
            if (hasApiKeyHeader)
            {
                _logger.LogDebug("Found the header {ApiKeyHeader}. Starting API Key validation", ApiKeyHeaderName);
 
                if (apiKeyValue.Count != 0 && !string.IsNullOrWhiteSpace(apiKeyValue))
                {
                    if (request.Headers.TryGetValue(ClientIdHeaderName, out var clientIdValue) && clientIdValue.Count != 0 && !string.IsNullOrWhiteSpace(clientIdValue))
                    {
                        if (await _apiKeyService.IsAuthorized(clientIdValue, apiKeyValue))
                        {
                            _logger.LogDebug("Client {ClientId} successfully logged in with key {ApiKey}", clientIdValue, apiKeyValue);
 
                            var apiKeyClaim = new Claim("apikey", apiKeyValue);
                            var subject = new Claim(ClaimTypes.Name, clientIdValue);
                            var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { apiKeyClaim, subject }, "ApiKey"));
                            context.HttpContext.User = principal;
 
                            return;
                        }
 
                        _logger.LogWarning("ClientId {ClientId} with ApiKey {ApiKey} is not authorized", clientIdValue, apiKeyValue);
                    }
                    else
                    {
                        _logger.LogWarning("{HeaderName} header not found or it was null or empty",ClientIdHeaderName);
                    }
                }
                else
                {
                    _logger.LogWarning("{HeaderName} header found, but api key was null or empty", ApiKeyHeaderName);
                }
            }
            else
            {
                _logger.LogWarning("No ApiKey header found.");
            }
 
            context.Result = new UnauthorizedResult();
        }
    }
}