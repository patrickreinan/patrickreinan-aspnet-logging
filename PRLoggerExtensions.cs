using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;

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
    }
}

