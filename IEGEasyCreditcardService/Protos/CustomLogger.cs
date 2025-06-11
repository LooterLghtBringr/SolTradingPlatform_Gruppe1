
namespace IEGEasyCreditcardService.Protos
{
    public class CustomLogger : ILogger
    {
        private readonly Logger.LoggerClient _client;

        public CustomLogger()
        {
             
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return default!;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                // Don't log anything to the back store the logLevel it's not enabled.
                return;
            }

            // validation
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            string message = formatter(state, exception);

            if(logLevel == LogLevel.Error || logLevel == LogLevel.Critical)
            {
                using var grpcChannel = Grpc.Net.Client.GrpcChannel.ForAddress("http://localhost:5000");
                // Configure Serilog to use gRPC sink
                var client = new Logger.LoggerClient(grpcChannel);
                var reply = client.LogMessage(new LogRequest { Message = message });
            }
        }
    }
}
