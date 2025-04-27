using Microsoft.AspNetCore.Mvc;
using ProductCatalogSqlite.Services;

namespace ProductCatalogSqlite.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly SqliteProductService _productService;

		public ProductsController(SqliteProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var products = await _productService.GetProductsAsync();
			return Ok(products);
		}
	}
}
