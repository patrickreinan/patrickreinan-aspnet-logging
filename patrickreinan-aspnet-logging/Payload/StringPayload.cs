using System;
namespace patrickreinan_aspnet_logging.Payload
{
    public class StringPayload : LogPayload
    {
        public override string Type => "Message";


        public string Message { get; }

        public StringPayload(string message)
        {
            Message = message;
        }
    }
}

