using IEGEasyCreditcardService.Protos;
using IEGEasyCreditcardService.Services;
using Serilog;

namespace IEGEasyCreditcardService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
			  .WriteTo.gRPC("http://localhost:5000")
              .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddScoped<ICreditcardValidator, CreditcardValidator>();

			var app = builder.Build();

			app.UseSwagger();
			app.UseSwaggerUI();

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}