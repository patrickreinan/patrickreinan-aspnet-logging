﻿using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using patrickreinan_aspnet_logging;
using static patrickreinan_aspnet_logging.LogObject;

namespace patrickreinan_aspnet_logging
{
    internal class PRLogger : ILogger
    {
        private readonly IHttpContextAccessor accessor;
        private readonly string category;
        private readonly LogLevel logLevel;
        private const string X_REQUEST_ID_HEADER = "x-request-id";


        private readonly JsonSerializerOptions jsonSerializerOptions;

        public PRLogger(string category, IHttpContextAccessor accessor, PRLoggerConfiguration configuration)
        {
            this.accessor = accessor;
            this.category = category;
            this.logLevel = configuration.LogLevel;

            jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull


            };
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

        public bool IsEnabled(LogLevel logLevel) => logLevel <= this.logLevel;


        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {

            if (accessor.HttpContext == null)
                return;

            var context = accessor.HttpContext;



            var requestHeaders = GetHeaders(context.Request.Headers);

            var request = new RequestObject(
                requestHeaders,
                context.Request.Path,
                context.Request.QueryString.HasValue ? context.Request.QueryString.Value! : string.Empty,
                context.Request.Host.Host,
                context.Request.Host.Port!.Value,
                context.Request.Method);


            ResponseObject? response =null;

            if (state is PRLoggerState loggerState && loggerState.LogResponse)
            {
                var responseHeaders = GetHeaders(context.Response.Headers);
                response = new ResponseObject(responseHeaders, context.Response.StatusCode);

            }

            var id = context.Request.Headers[X_REQUEST_ID_HEADER];

            var log = new LogObject(
                id: id == StringValues.Empty ? Guid.NewGuid().ToString() : id!,
                Enum.GetName(typeof(LogLevel), logLevel) ?? String.Empty,
                request: request,
                response: response,
                message: formatter != null ? formatter(state, exception) : null);

            Console.WriteLine(JsonSerializer.Serialize(log, jsonSerializerOptions));


        }

        private HeaderObject[] GetHeaders(IHeaderDictionary headers)
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
