using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using patrickreinan_aspnet_logging;
using patrickreinan_aspnet_logging.Payload;

namespace patrickreinan_aspnet_logging
{
    internal class PRLogger : ILogger
    {
        private readonly IHttpContextAccessor accessor;
        private readonly string category;
        private readonly LogLevel logLevel;


        private readonly JsonSerializerOptions jsonSerializerOptions;

        public PRLogger(string category, IHttpContextAccessor accessor, PRLoggerConfiguration configuration)
        {
            this.accessor = accessor;
            this.category = category;
            this.logLevel = configuration.LogLevel;

            jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                


            };
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

        public bool IsEnabled(LogLevel logLevel) => logLevel <= this.logLevel;


        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
         
            if (accessor.HttpContext == null)
                return;

            LogPayload payload = state switch
            {
                LogPayload logPayload => logPayload,
                _ => new StringPayload(formatter(state,exception))
            };

            
            var context = accessor.HttpContext;

            var id = context.RequestServices.GetRequiredService<PRLoggerIdManager>().GetId();
            var level = Enum.GetName(typeof(LogLevel), logLevel) ?? String.Empty;


            var log = new LogObject(
                id,
                level,       
                category,
                payload);

            Console.WriteLine(JsonSerializer.Serialize(log, jsonSerializerOptions));
            
        }

       
    }
}

