using Microsoft.EntityFrameworkCore;
using ProductCatalogSqlite.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalogSqlite.Services
{
	public class SqliteProductService
	{
		private readonly ProductContext _context;

		public SqliteProductService(ProductContext context)
		{
			_context = context;
		}

		public async Task<List<string>> GetProductsAsync()
		{
			var products = await _context.Products.ToListAsync();
			return products.Select(p => p.Name).ToList();
		}
	}
}
