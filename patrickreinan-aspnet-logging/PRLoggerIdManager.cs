using System;
namespace patrickreinan_aspnet_logging
{
	internal class PRLoggerIdManager
	{
		private readonly string id;

		public PRLoggerIdManager()
		{
			id = Guid.NewGuid().ToString();
		}

		public  string GetId()
		{
			return id;
		}
	}
}

