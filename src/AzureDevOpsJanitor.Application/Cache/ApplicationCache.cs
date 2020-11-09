﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace AzureDevOpsJanitor.Application.Cache
{
    public class ApplicationCache : MemoryCache
    {
        public ApplicationCache(IOptions<MemoryCacheOptions> optionsAccessor) : base(optionsAccessor)
        {
        }
    }
}
