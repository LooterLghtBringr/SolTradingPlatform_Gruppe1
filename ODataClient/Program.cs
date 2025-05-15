using Microsoft.OData.Client;
using System.ComponentModel;

namespace ODataClient
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var serviceUri = new Uri("https://localhost:5000/odata/");
			var context = new Container(serviceUri);

			// Beispielabfragen:
			// 1. Alle Produkte unter 100€
			var cheapProducts = context.Products
				.AddQueryOption("$filter", "Price lt 100")
				.ToList();

			// 2. Nach Kategorie gruppiert
			var byCategory = context.Products
				.AddQueryOption("$orderby", "Category")
				.ToList();

			// 3. Nur Name und Preis
			var projected = context.Products
				.AddQueryOption("$select", "Name,Price")
				.ToList();
		}
	}
}
