﻿using AutoMapper;
using AzureDevOpsJanitor.Infrastructure.Vsts.Events;
using System.Text.Json;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Mapping.Converters
{
    public class JsonElementToWebHookEventConverter : ITypeConverter<JsonElement, WebHookEvent>
    {
        public WebHookEvent Convert(JsonElement source, WebHookEvent destination, ResolutionContext context)
        {
            return JsonSerializer.Deserialize<WebHookEvent>(source.GetRawText());
        }
    }
}
