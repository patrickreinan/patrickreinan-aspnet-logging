namespace patrickreinan_aspnet_logging;
internal partial class LogObject
{
    internal class HeaderObject
    {
        public HeaderObject(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public string Value { get; private set; }

    }


}

