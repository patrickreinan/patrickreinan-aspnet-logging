using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace patrickreinan_aspnet_logging
{
	public abstract class LogPayload 
    {
        

        public abstract string Type { get; }

    
    }
}

