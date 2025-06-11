using IEGEasyCreditcardService.Protos;
using IEGEasyCreditcardService.Services;

namespace IEGEasyCreditcardService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
			builder.Logging.AddCustomLogger();
			builder.Logging.SetMinimumLevel(LogLevel.Critical);

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