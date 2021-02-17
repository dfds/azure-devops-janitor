﻿using MediatR;
using Microsoft.Extensions.Logging;
using CloudEngineering.CodeOps.Abstractions.Facade;

namespace AzureDevOpsJanitor.Application
{
    public sealed class ApplicationFacade : Facade
    {
        public ApplicationFacade(IMediator mediator, ILogger<ApplicationFacade> logger) : base(mediator, logger)
        {
            
        }
    }
}
