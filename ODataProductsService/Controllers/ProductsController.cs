using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataProductsService.Data;

namespace ODataProductsService.Controllers
{
	public class ProductsController : ODataController
	{
		private readonly ProductContext _context;

		public ProductsController(ProductContext context)
		{
			_context = context;
		}

		[EnableQuery] // Aktiviert OData-Abfrageoptionen
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_context.Products);
		}
	}
}
