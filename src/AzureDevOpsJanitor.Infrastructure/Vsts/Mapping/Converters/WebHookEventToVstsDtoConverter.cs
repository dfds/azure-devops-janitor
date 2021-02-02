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
                    var projectId = source.ResourceContainers?.GetProperty("project").GetProperty("id").GetGuid();

                    if(projectId.HasValue)
                    { 
                        buildDto.Project = new ProjectDto()
                        {
                            Id = projectId.Value
                        };
                    }

                    //TODO: Figure out how to map to capability identifier via tags without having to make a call to VSTS 
                    //We could enrich it from the db, this would create orphans when builds are started directly from the portal, 
                    //but then again even with a direct call this could happen if people forget the capability identifier tag.

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
