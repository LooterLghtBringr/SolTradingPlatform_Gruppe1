using GrpcService.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;

namespace GrpcService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            builder.WebHost
                .ConfigureKestrel(options => { options.ListenLocalhost(5000, o => o.Protocols = HttpProtocols.Http2); })   // Setup a HTTP/2 endpoint without TLS.
                .ConfigureLogging(logging => { logging.AddSerilog(logger); })
                .ConfigureServices(services => { services.AddHttpContextAccessor(); })
;

            // Add services to the container.
            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<LoggerService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}