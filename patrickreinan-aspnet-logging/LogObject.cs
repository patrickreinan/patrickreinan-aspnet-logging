namespace patrickreinan_aspnet_logging;

internal class LogObject
{
    public LogObject(string id, string logLevel, RequestObject request, ResponseObject? response, string? message, string category)
    {

        Timespan = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        Request = request;
        Response = response;
        Id = id;
        LogLevel = logLevel;

        Message = message;
        Category = category;
    }
    public string Id { get;  }

    public string LogLevel { get;  }

    public string? Message { get; }

    public string Category { get; }

    public long Timespan { get;  }

    public RequestObject Request { get; }

    public ResponseObject? Response { get;  }


}

