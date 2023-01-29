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
        public static ILoggingBuilder AddPRLogger(
        this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.AddHttpContextAccessor();
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
                          context.Request.Headers.ToHeaderObjectArray(),
                          context.Request.Path,
                          context.Request.QueryString.HasValue ? context.Request.QueryString.Value! : string.Empty,
                          context.Request.Host.Host,
                          context.Request.Host.Port.HasValue ? context.Request.Host.Port.Value : 0,
                          context.Request.Method);

                logger.Log(LogLevel.Information, eventId, requestPayload, null, (stateArg, ex) => string.Empty);




                context.Response.OnCompleted(async () =>
                {


                    
                    var responsePayload = new ResponseLogPayload(
                        context.Response.Headers.ToHeaderObjectArray(),
                        context.Response.StatusCode);

                    var logLevel = context.Response.StatusCode switch
                    {
                        >= 200 and <= 299 => LogLevel.Information,
                        >= 300 and <= 399 => LogLevel.Warning,
                        >= 400 and <= 499 => LogLevel.Warning,
                        >= 500 and <= 599 => LogLevel.Error,
                        _ => LogLevel.Information
                    };

                    if (!logger.IsEnabled(logLevel))
                    {
                        await Task.CompletedTask;
                        return;
                    }


                   
                    Exception? exception = null;


                    logger.Log(logLevel, eventId, responsePayload, exception, (stateArg, ex) => string.Empty);

                    await Task.CompletedTask;
                });
                await next.Invoke();
            });

            return app;
        }
    }

    
}

