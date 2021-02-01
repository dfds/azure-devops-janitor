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
                    var buildDto = JsonSerializer.Deserialize<BuildDto>(source.Payload.Value.GetRawText());
                    var projectId = source.ResourceContainers.Value.GetProperty("project").GetProperty("id").GetGuid();

                    buildDto.Project = new ProjectDto()
                    {
                        Id = projectId
                    };

                    //TODO: Figure out how to get tags without having to make a call to VSTS

                    return buildDto;

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
