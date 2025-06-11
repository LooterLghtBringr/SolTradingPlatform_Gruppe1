using Microsoft.Extensions.Options;

namespace IEGEasyCreditcardService.Protos
{
    public class CustomLoggingProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger();
        }

        public void Dispose()
        {

        }
    }

    public static class LoggerExtensions
    {
        public static ILoggingBuilder AddCustomLogger(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, CustomLoggingProvider>();

            return builder;
        }
    }
}
