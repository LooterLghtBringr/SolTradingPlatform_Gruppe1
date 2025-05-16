using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ProductCatalogJson.Services;

namespace ProductCatalogJson
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("odata",
				EdmModelBuilder.GetEdmModel()).Select().Filter().OrderBy().Expand());

			builder.Services.AddSingleton<LocalProductService>();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			app.UseSwagger();
			app.UseSwaggerUI();

			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.MapControllers();

			app.UseRouting();
			app.UseEndpoints(endpoints => endpoints.MapControllers());

			app.Run();
		}

		public static class EdmModelBuilder
		{
			public static IEdmModel GetEdmModel()
			{
				var builder = new ODataConventionModelBuilder();
				builder.EntitySet<Models.Product>("Products");
				return builder.GetEdmModel();
			}
		}
	}
}
