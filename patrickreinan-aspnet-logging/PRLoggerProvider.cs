using System;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace patrickreinan_aspnet_logging
{
	internal class PRLoggerProvider : ILoggerProvider
	{
        private readonly IHttpContextAccessor accessor;
        private readonly PRLoggerConfiguration configuration;
        private ConcurrentDictionary<string, PRLogger> loggers = new(StringComparer.OrdinalIgnoreCase);
        public PRLoggerProvider(IHttpContextAccessor accessor, PRLoggerConfiguration configuration)
        {
            this.accessor = accessor;
            this.configuration = configuration;
        }

        public ILogger CreateLogger(string categoryName) => loggers.GetOrAdd(categoryName, name => new PRLogger(name, accessor, configuration));

        public void Dispose()
        {
            
            loggers.Clear();

        }
    }
}

