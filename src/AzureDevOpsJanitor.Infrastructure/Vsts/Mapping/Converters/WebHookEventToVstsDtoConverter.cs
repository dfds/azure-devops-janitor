using AutoMapper;
using AzureDevOpsJanitor.Infrastructure.Vsts.DataTransferObjects;
using AzureDevOpsJanitor.Infrastructure.Vsts.Events;
using System.Text.Json;

namespace AzureDevOpsJanitor.Infrastructure.Vsts.Mapping.Converters
{
    public class WebHookEventToVstsDtoConverter : ITypeConverter<WebHookEvent, VstsDto>
    {
        internal const string WebHookEventBuildCompletedIdentifier = "build.complete";

        public VstsDto Convert(WebHookEvent source, VstsDto destination, ResolutionContext context)
        {
            //TODO: Implement support for all required WebHookEvents
            switch(source.EventType)
            {
                case WebHookEventBuildCompletedIdentifier:
                    var dto = JsonSerializer.Deserialize<BuildDto>(source.Payload.Value.GetRawText());
                    var projectId = source.ResourceContainers.Value.GetProperty("project").GetProperty("id").GetGuid();

                    dto.Project = new ProjectDto()
                    {
                        Id = projectId
                    };

                    return dto;

                default:

                    return null;
            }
        }
    }
}
