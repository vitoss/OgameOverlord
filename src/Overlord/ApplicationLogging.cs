using Microsoft.Extensions.Logging;

namespace Overlord {
    public static class ApplicationLogging
    {
        
        static ApplicationLogging() {
             LoggerFactory = new LoggerFactory()
                                    .AddConsole(minLevel:LogLevel.Debug)
                                    .AddDebug();
        }
        
        public static ILoggerFactory LoggerFactory { get; }
        public static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
    }
}