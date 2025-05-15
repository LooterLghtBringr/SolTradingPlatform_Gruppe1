using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataProductsService.Services;

namespace ODataProductsService.Controllers
{
	public class ProductsController : ODataController
	{
		private readonly JsonProductService _service;

		public ProductsController(JsonProductService service)
		{
			_service = service;
		}

		[EnableQuery] // Aktiviert OData-Abfragen ($filter, $orderby etc.)
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_service.GetProducts());
		}
	}
}
