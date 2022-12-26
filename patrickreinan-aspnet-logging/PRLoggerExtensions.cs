using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.AspNetCore.Builder;
using patrickreinan_aspnet_logging.Payload;

namespace patrickreinan_aspnet_logging
{
	public static class PRLoggerExtensions
	{

        const string PRLOGGER_LOG_ID= "prlogger-log-id";
        public static ILoggingBuilder AddPRLogger(
        this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<PRLoggerIdManager>();
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, PRLoggerProvider>());

           

            return builder;
        }

        public static ILoggingBuilder AddPRConsoleLogger(
            this ILoggingBuilder builder,
            Func<PRLoggerConfiguration> configure)
        {
            builder.AddPRLogger();
            builder.Services.AddSingleton<PRLoggerConfiguration>(configure());
            return builder;
        }
  
        public static WebApplication UsePRLogging(this WebApplication app, string categoryName)
        {

           app.Use(async (context, next) =>
            {

                var factory = app.Services.GetRequiredService<ILoggerFactory>();


                var logger = factory.CreateLogger(categoryName);

                var eventId = new EventId();

                var requestPayload = new RequestLogPayload(
                          context.Request.Headers.ToHeadersObject(),
                          context.Request.Path,
                          context.Request.QueryString.HasValue ? context.Request.QueryString.Value! : string.Empty,
                          context.Request.Host.Host,
                          context.Request.Host.Port.HasValue ? context.Request.Host.Port.Value : 0,
                          context.Request.Method);

                logger.Log(LogLevel.Information, eventId, requestPayload, null, (stateArg, ex) => string.Empty);


                var id = context.RequestServices.GetRequiredService<PRLoggerIdManager>().GetId();
                context.Response.Headers.Add(PRLOGGER_LOG_ID, id);


                context.Response.OnCompleted(async () =>
                {

                    
                    var responsePayload = new ResponseLogPayload(
                        context.Response.Headers.ToHeadersObject(),
                        context.Response.StatusCode);

                    

                   
                    Exception? exception = null;


                    logger.Log(LogLevel.Information, eventId, responsePayload, exception, (stateArg, ex) => string.Empty);

                    await Task.CompletedTask;
                });
                await next.Invoke();
            });

            return app;
        }
    }

    
}

