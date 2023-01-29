using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace patrickreinan_aspnet_logging
{
	public abstract class LogPayload 
    {
        private readonly JsonSerializerOptions jsonSerializerOptions;

        public LogPayload()
        {
            jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull


            };
        }

        public abstract string Type { get; }

    
    }
}

