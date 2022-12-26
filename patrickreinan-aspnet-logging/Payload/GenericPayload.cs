using System;
namespace patrickreinan_aspnet_logging.Payload
{
	public class GenericPayload<T> : LogPayload
	{

        public GenericPayload(T data)
        {
            Data = data;
        }

        public override string Type => typeof(T).Name;

        public T? Data { get; }
    }
}

