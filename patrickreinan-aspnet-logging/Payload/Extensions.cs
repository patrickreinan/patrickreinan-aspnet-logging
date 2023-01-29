using System;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace patrickreinan_aspnet_logging.Payload
{
	internal static class Extensions
	{
		public static HeaderObject[] ToHeaderObjectArray(this IHeaderDictionary headers)
        {
            var keys = headers.Keys.ToArray();

            var result = new HeaderObject[keys.Count()];
            for (int index = 0; index < keys.Count(); index++)
            {

                var key = keys[index];
                var builder = new StringBuilder();
                var name = key;
                foreach (var value in headers[key])
                {
                    if (builder.Length > 0)
                        builder.Append(',');

                    builder.Append(value);
                }

                result[index] = new HeaderObject(name, builder.ToString());


            }

            return result;
        }
    }
}

