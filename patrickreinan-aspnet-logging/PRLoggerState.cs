using System;
namespace patrickreinan_aspnet_logging
{
	internal class PRLoggerState 
	{
        public PRLoggerState(Boolean logResponse)
        {
            LogResponse = logResponse;

        }
        
        public bool LogResponse { get; }
    }
}

