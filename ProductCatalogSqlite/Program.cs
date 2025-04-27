using Microsoft.EntityFrameworkCore;
using ProductCatalogSqlite.Data;
using ProductCatalogSqlite.Services;

namespace ProductCatalogSqlite
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<ProductContext>(); // SQLite
			builder.Services.AddScoped<SqliteProductService>(); // Product Service

			var app = builder.Build();

			app.UseSwagger();
			app.UseSwaggerUI();

			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.MapControllers();

			using (var scope = app.Services.CreateScope())
			{
				var db = scope.ServiceProvider.GetRequiredService<ProductContext>();
				db.Database.EnsureCreated(); // Erstellt die DB, falls nicht vorhanden
			}

			app.Run();
		}
	}
}
