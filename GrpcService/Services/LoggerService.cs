using Grpc.Core;

namespace GrpcService.Services
{
    public class LoggerService : Logger.LoggerBase
    {
        private readonly ILogger<LoggerService> _logger;
        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public override Task<LogReply> LogMessage(LogRequest request, ServerCallContext context)
        {
            _logger.LogCritical(request.Message);

            return Task.FromResult(new LogReply
            {
                Message = "Got message"
            });
        }
    }
}
