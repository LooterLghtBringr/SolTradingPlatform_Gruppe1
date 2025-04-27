using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProductCatalogSqlite.Models;

namespace ProductCatalogSqlite.Data
{
	public class ProductContext : DbContext
	{
		public DbSet<Product> Products { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite("Data Source=./Data/products.db");
	}
}
