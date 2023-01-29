namespace patrickreinan_aspnet_logging;

    internal class ResponseLogPayload : LogPayload
    {
        public ResponseLogPayload(HeaderObject[] headers, int statusCode)
        {
            Headers = headers;
            StatusCode = statusCode;
        }

        public HeaderObject[] Headers { get;  }
        public int StatusCode { get;  }

    public override string Type => "Response";
}




