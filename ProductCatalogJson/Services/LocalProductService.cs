using ProductCatalogJson.Models;
using System.Text.Json;

namespace ProductCatalogJson.Services
{
	public class LocalProductService
	{
		private readonly string _dataFilePath;
		private List<Product> _products;

		public LocalProductService(IConfiguration config)
		{
			_dataFilePath = config["LocalProductStore:DataFilePath"] ?? "products.json";
			EnsureDataFileExists();
			LoadProducts();
		}

		public Product AddProduct(Product newProduct)
		{
			var products = GetProducts().ToList();

			//Neue ID generieren (max ID + 1)
			newProduct.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;

			products.Add(newProduct);

			SaveProducts(products);

			return newProduct;
		}

		public bool DeleteProduct(int id)
		{
			var products = GetProducts().ToList();

			var productToRemove = products.FirstOrDefault(p => p.Id == id);
			if (productToRemove == null)
				return false;

			products.Remove(productToRemove);

			SaveProducts(products);

			return true;
		}

		public Product UpdateProduct(Product updatedProduct)
		{
			var products = GetProducts().ToList();

			var productToUpdate = products.FirstOrDefault(p => p.Id == updatedProduct.Id);
			if (productToUpdate == null)
				return null;

			productToUpdate.Name = updatedProduct.Name;

			SaveProducts(products);

			return updatedProduct;
		}

		private void SaveProducts(List<Product> products)
		{
			var options = new JsonSerializerOptions
			{
				WriteIndented = true // Für besser lesbare JSON-Datei
			};

			var json = JsonSerializer.Serialize(products, options);
			File.WriteAllText(_dataFilePath, json);
		}

		public IQueryable<Product> GetProducts()
		{
			LoadProducts();
			return _products.AsQueryable();
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

		private void LoadProducts()
		{
			var json = File.ReadAllText(_dataFilePath);
			_products = JsonSerializer.Deserialize<List<Product>>(json)!;
		}
	}
}
