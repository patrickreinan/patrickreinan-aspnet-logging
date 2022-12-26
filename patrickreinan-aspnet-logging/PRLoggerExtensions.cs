using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.AspNetCore.Builder;

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
                context.Response.OnCompleted(async () =>
                {
                    var factory = app.Services.GetRequiredService<ILoggerFactory>();


                    var logger =factory.CreateLogger(categoryName);

                    

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


                    var state = new PRLoggerState(true);
                    var eventId = new EventId(0, logLevel.ToString());
                    Exception? exception = null;

      
                    logger.Log<PRLoggerState>(logLevel, eventId, state, exception, (stateArg, ex) => string.Empty);



                    await Task.CompletedTask;
                });
                await next.Invoke();
            });

            return app;
        }
    }

    
}

