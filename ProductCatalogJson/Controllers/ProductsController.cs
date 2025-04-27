using Microsoft.AspNetCore.Mvc;
using ProductCatalogJson.Services;

namespace ProductCatalogJson.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly LocalProductService _productService;

		public ProductsController(LocalProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_productService.GetProducts());
		}
	}
}
