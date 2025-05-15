using Microsoft.EntityFrameworkCore;
using ODataProductsService.Models;

namespace ODataProductsService.Data
{
	public class ProductContext : DbContext
	{
		public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
		public DbSet<Product> Products { get; set; }
	}
}
