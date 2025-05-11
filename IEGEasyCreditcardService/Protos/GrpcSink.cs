using Grpc.Net.Client;
using Serilog.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;

namespace IEGEasyCreditcardService.Protos
{
    public class GrpcSink : PeriodicBatchingSink
    {
        private readonly Logger.LoggerClient _client;
        private readonly IFormatProvider _formatProvider;

        public GrpcSink(
            string grpcEndpoint,
            IFormatProvider formatProvider,
            int? queueLimit = 2,
            int? batchSizeLimit = 1000,
            TimeSpan? period = null
        ) : this(grpcEndpoint, formatProvider, queueLimit ?? int.MaxValue, batchSizeLimit ?? 1000,
            period ?? TimeSpan.FromSeconds(2))
        {
        }

        public GrpcSink(
            string grpcEndpoint,
            IFormatProvider formatProvider,
            int queueLimit,
            int batchSizeLimit,
            TimeSpan period
        ) : base(batchSizeLimit, period, queueLimit)
        {
            if (string.IsNullOrEmpty(grpcEndpoint))
            {
                throw new ArgumentNullException(nameof(grpcEndpoint));
            }

            _client = new Logger.LoggerClient(GrpcChannel.ForAddress(grpcEndpoint));
            _formatProvider = formatProvider;
        }

        protected override async Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            var logEvents = events as LogEvent[] ?? events.ToArray();
            foreach (var le in logEvents)
            {
                var message = le.RenderMessage(_formatProvider);
                await _client.LogMessageAsync(new LogRequest { Message = message });
            }            
        }
    }

    public static class GrpcSinkExtensions
    {
        public static LoggerConfiguration gRPC(
            this LoggerSinkConfiguration sinkLoggerConfiguration,
            string grpcEndpoint, 
            IFormatProvider labelProvider = null,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Fatal,
            bool stackTraceAsLabel = false)
        {
            return sinkLoggerConfiguration.Sink(
                new GrpcSink(
                    grpcEndpoint,
                    labelProvider,
                    period: TimeSpan.FromSeconds(2),
                    queueLimit: int.MaxValue
                ), restrictedToMinimumLevel
            );
        }

        public static LoggerConfiguration gRPC(
            this LoggerSinkConfiguration sinkLoggerConfiguration,
            string grpcEndpoint,
            IFormatProvider labelProvider,
            TimeSpan period,
            int queueLimit,
            int batchSizeLimit,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Fatal
        )
        {
            return sinkLoggerConfiguration.Sink(
                new GrpcSink(
                    grpcEndpoint,
                    labelProvider,
                    queueLimit,
                    batchSizeLimit,
                    period
                ), restrictedToMinimumLevel
            );
        }
    }
}
