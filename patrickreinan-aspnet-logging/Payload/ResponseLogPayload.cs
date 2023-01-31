namespace patrickreinan_aspnet_logging;

    internal class ResponseLogPayload : LogPayload
    {
        public ResponseLogPayload(dynamic headers, int statusCode)
        {
            Headers = headers;
            StatusCode = statusCode;
        }

        public dynamic Headers { get;  }
        public int StatusCode { get;  }

    public override string Type => "Response";
}




