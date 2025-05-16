using Microsoft.AspNetCore.Mvc;
using ProductCatalogJson.Models;
using ProductCatalogJson.Services;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ProductCatalogJson.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ODataController
	{
		private readonly LocalProductService _productService;

		public ProductsController(LocalProductService productService)
		{
			_productService = productService;
		}

		[EnableQuery] // Aktiviert OData-Abfragen ($filter, $orderby etc.)
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_productService.GetProducts());
		}

		[HttpPost]
		public IActionResult AddProduct([FromBody] Product newProduct)
		{
			try
			{
				// Validierung
				if (newProduct == null || string.IsNullOrEmpty(newProduct.Name))
				{
					return BadRequest("Invalid product data");
				}

				// Produkt hinzufügen
				var addedProduct = _productService.AddProduct(newProduct);

				// HTTP 201 Created mit Location-Header zurückgeben
				return CreatedAtAction(nameof(Get), new { id = addedProduct.Id }, addedProduct);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPut]
		public IActionResult UpdateProduct([FromBody] Product newProduct)
		{
			try
			{
				// Validierung
				if (newProduct == null || string.IsNullOrEmpty(newProduct.Name))
				{
					return BadRequest("Invalid product data");
				}

				// Produkt hinzufügen
				var updatedProduct = _productService.UpdateProduct(newProduct);

				if (updatedProduct == null)
					return NotFound();

				// HTTP 201 Created mit Location-Header zurückgeben
				return CreatedAtAction(nameof(Get), new { id = updatedProduct.Id }, updatedProduct);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var success = _productService.DeleteProduct(id);
			return success ? NoContent() : NotFound();
		}
	}
}
