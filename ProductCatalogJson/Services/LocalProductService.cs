using ProductCatalogJson.Models;
using System.Text.Json;

namespace ProductCatalogJson.Services
{
	public class LocalProductService
	{
		private readonly string _dataFilePath;

		public LocalProductService(IConfiguration config)
		{
			_dataFilePath = config["LocalProductStore:DataFilePath"] ?? "products.json";
			EnsureDataFileExists();
		}

		public IEnumerable<Product> GetProducts()
		{
			var json = File.ReadAllText(_dataFilePath);
			return JsonSerializer.Deserialize<List<Product>>(json);
		}

		private void EnsureDataFileExists()
		{
			if (!File.Exists(_dataFilePath))
			{
				var defaultProducts = new List<Product>
				{
					new Product { Id = 1, Name = "Windows Phone" },
					new Product { Id = 2, Name = "BlackBerry" },
					new Product { Id = 3, Name = "iPhone" }
				};
				File.WriteAllText(_dataFilePath, JsonSerializer.Serialize(defaultProducts));
			}
		}
	}
}
