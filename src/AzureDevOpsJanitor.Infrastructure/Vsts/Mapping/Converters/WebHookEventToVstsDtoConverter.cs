using AutoMapper;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts.Events;
using System;
using System.Text.Json;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Mapping.Converters
{
    public class WebHookEventToVstsDtoConverter : ITypeConverter<WebHookEvent, VstsDto>
    {
        public VstsDto Convert(WebHookEvent source, VstsDto destination, ResolutionContext context)
        {
            switch(source.EventType)
            {
                case BuildCompletedEvent.EventIdentifier:
                    var dto = JsonSerializer.Deserialize<BuildDto>(source.Payload.Value.GetRawText());
                    var projectId = source.ResourceContainers.Value.GetProperty("project").GetProperty("id").GetGuid();

                    dto.Project = new ProjectDto()
                    {
                        Id = projectId
                    };

                    return dto;

                case ReleaseCreatedEvent.EventIdentifier:
                    throw new NotImplementedException();

                case ReleaseCompletedEvent.EventIdentifier:
                    throw new NotImplementedException();

                case ReleaseAbandonedEvent.EventIdentifier:
                    throw new NotImplementedException();

                case ReleaseApprovalPendingEvent.EventIdentifier:
                    throw new NotImplementedException();

                case ReleaseApprovalCompletedEvent.EventIdentifier:
                    throw new NotImplementedException();

                default:
                    return null;
            }
        }
    }
}
