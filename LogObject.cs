namespace patrickreinan_aspnet_logging;
internal partial class LogObject
{
    public LogObject(string id, string logLevel, RequestObject request, ResponseObject response, string message)
    {
        DateTime = DateTime.Now.ToUniversalTime();
        Request = request;
        Response = response;
        Id = id;
        LogLevel = logLevel;
        Message = message;
    }
    public string Id { get;  }

    public string LogLevel { get;  }

    public string Message { get; }

    public DateTime DateTime { get;  }

    public RequestObject Request { get; }

    public ResponseObject Response { get;  }


}

