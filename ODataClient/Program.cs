using Microsoft.OData.Client;
using System.ComponentModel;

namespace ODataClient
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var serviceUri = new Uri("http://localhost:5026/odata/");
			var context = new Container(serviceUri);

			// Beispielabfragen:
			// 1. Alle Produkte mit ID < 5
			Console.WriteLine("Produkte mit ID < 5:");
			var cheapProducts = context.Products
				.AddQueryOption("$filter", "Id lt 5")
				.ToList();
			Console.WriteLine("Anzahl Produkte: " + cheapProducts.Count);
			Console.WriteLine("Produkte: " + string.Join(", ", cheapProducts.Select(p => p.Name)));
			Console.WriteLine();

			// 2. Nach Name sortiert
			Console.WriteLine("Produkte nach Name sortiert");
			var byCategory = context.Products
				.AddQueryOption("$orderby", "Name")
				.ToList();
			Console.WriteLine("Anzahl Produkte: " + byCategory.Count());
			Console.WriteLine("Produkte: " + string.Join(", ", byCategory.Select(p => p.Name)));
			Console.WriteLine();

			// 3. Name und ID der Produkte
			Console.WriteLine("Nur der Name der Produkte");
			var projected = context.Products
				.AddQueryOption("$select", "Id, Name")
				.ToList();
			Console.WriteLine("Anzahl Produkte: " + projected.Count());
			Console.WriteLine("Produkte: " + string.Join(", ", projected.Select(p => p.Id + "-" + p.Name)));

			Console.ReadKey();
		}
	}
}
