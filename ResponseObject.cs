namespace patrickreinan_aspnet_logging;
internal partial class LogObject
{
    internal class ResponseObject
    {
        public ResponseObject(HeaderObject[] headers, int statusCode)
        {
            Headers = headers;
            StatusCode = statusCode;
        }

        public HeaderObject[] Headers { get;  }
        public int StatusCode { get;  }

    }


}

