namespace ErrorCoordinator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // dotnet IEGEasyCreditcardService.dll --urls "https://localhost:5002"
            var serviceUrls = new[] {
                "https://localhost:5001",
                "https://localhost:5002",
                "https://localhost:5003"
            };

            var faultOrchestrator = new FaultOrchestrator(serviceUrls);

            await faultOrchestrator.Start();

            Console.WriteLine("Fault Orchestrator has completed its execution.");
            Console.ReadKey();
        }
    }
}