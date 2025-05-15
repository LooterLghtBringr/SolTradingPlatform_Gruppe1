using Microsoft.Extensions.Options;
using ODataProductsService.Models;
using System.Text.Json;

namespace ODataProductsService.Services
{
	public class JsonProductService
	{
		private readonly JsonDataOptions _options;
		private List<Product> _products;

		public JsonProductService(IOptions<JsonDataOptions> options)
		{
			_options = options.Value;
			LoadProducts();
		}

		public IQueryable<Product> GetProducts()
		{
			return _products.AsQueryable();
		}

		private void LoadProducts()
		{
			var json = File.ReadAllText(_options.FilePath);
			_products = JsonSerializer.Deserialize<List<Product>>(json)!;
		}
	}
}
