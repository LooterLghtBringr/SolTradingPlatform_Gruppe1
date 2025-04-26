
using Microsoft.AspNetCore.Mvc.Formatters;
using WebApiContrib.Core.Formatter.Csv;

namespace PaymentService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true; // Allow content negotiation
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter()); // Add XML support
                options.OutputFormatters.Add(new CsvOutputFormatter(new CsvFormatterOptions())); // Add CSV support
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
