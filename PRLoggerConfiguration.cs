using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace patrickreinan_aspnet_logging
{
	public sealed class PRLoggerConfiguration
	{
		public PRLoggerConfiguration(LogLevel logLevel)
		{
            LogLevel = logLevel;
        
        }

        public LogLevel LogLevel { get; }
   
    }
}

