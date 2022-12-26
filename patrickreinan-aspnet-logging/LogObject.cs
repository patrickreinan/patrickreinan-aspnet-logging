using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace patrickreinan_aspnet_logging;

internal class LogObject 
{

    public LogObject(
        [NotNull] string id,
        [NotNull] string logLevel,
        [NotNull] string category,
        [NotNull] object payload)
           {




        Timespan = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        Id = id;
        LogLevel = logLevel;
        Category = category;
        Payload = payload;
        
    }

    public string Id { get;  }

    public string LogLevel { get;  }

    public object Payload { get; }

    public string Category { get; }

    public long Timespan { get;  }



}

