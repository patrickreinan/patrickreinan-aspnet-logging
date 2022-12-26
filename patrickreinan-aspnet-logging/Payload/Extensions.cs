using System;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Dynamic;

namespace patrickreinan_aspnet_logging.Payload
{
	internal static class Extensions
	{
		public static ExpandoObject ToHeadersObject(this IHeaderDictionary headers)
        {




            var keys = headers.Keys.ToArray();
            var headersObject = new ExpandoObject();

            
            for (int index = 0; index < keys.Count(); index++)
            {
                var item = new ExpandoObject();
                var key = keys[index];
                var builder = new StringBuilder();

                foreach (var value in headers[key])
                {

                    if (builder.Length > 0)
                        builder.Append(',');

                    builder.Append(value);
                }

                ((IDictionary<string, object>)headersObject!).Add(key, builder.ToString());
            }

            return headersObject;
        }
    }
}

