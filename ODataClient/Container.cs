using Microsoft.OData.Client;
using ODataProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataClient
{
	public class Container : DataServiceContext
	{
		public Container(Uri serviceRoot) : base(serviceRoot)
		{
			this.Products = base.CreateQuery<Product>("Products");
		}
		public DataServiceQuery<Product> Products { get; }
	}
}
