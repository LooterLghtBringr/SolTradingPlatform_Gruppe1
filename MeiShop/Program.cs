
namespace MeiShop
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddHttpClient("ProductCatalogJson", client =>
			{
				client.BaseAddress = new Uri("http://localhost:5026"); // Adjust port
			});

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