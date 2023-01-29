namespace patrickreinan_aspnet_logging;

internal class RequestLogPayload : LogPayload
{
    public RequestLogPayload(HeaderObject[] headers, string path, string query, string host, int port, string method) 
    {
        Headers = headers;
        Path = path;
        Query = query;
        Host = host;
        Port = port;
        Method = method;

    }
    public HeaderObject[] Headers { get; }
    public string Path { get; }

    public string Query { get; }

    public string Host { get; }
    public int Port { get; }
    public string Method { get; }

    public override string Type => "Request";


}

