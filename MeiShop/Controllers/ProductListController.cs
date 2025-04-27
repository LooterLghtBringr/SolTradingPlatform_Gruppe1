using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogJson.Models;

namespace MeiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductListController : ControllerBase
    {

		private readonly IHttpClientFactory _httpClientFactory;
		private readonly string[] _fallbackProducts = { "Windows Phone", "BlackBerry", "iPhone" };

		public ProductListController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				// Versuche, Produkte vom Local Datastore Service zu holen
				var client = _httpClientFactory.CreateClient("ProductCatalogJson");
				var response = await client.GetAsync("/api/products");

				if (response.IsSuccessStatusCode)
				{
					var products = await response.Content.ReadFromJsonAsync<List<Product>>();
					return Ok(products.Select(p => p.Name));
				}

				// Fallback: Hard-coded Werte
				return Ok(_fallbackProducts);
			}
			catch
			{
				// Resilienz: Bei Fehlern Fallback zurückgeben
				return Ok(_fallbackProducts);
			}
		}
	}
}
