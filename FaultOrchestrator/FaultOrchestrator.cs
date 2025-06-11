
using Polly;

namespace ErrorCoordinator
{
    internal class FaultOrchestrator
    {
        private readonly RoundRobin _clientPool;

        public FaultOrchestrator(string[] serviceUrls)
        {
            _clientPool = new RoundRobin(serviceUrls);
        }

        public async Task Start(int numberOfRounds)
        {
            var client = _clientPool.GetNextClient();

            for (int attempt = 0; attempt < numberOfRounds; attempt++)
            {               
                var retryPolicy = Policy
                        .Handle<Exception>()
                        .OrResult<HttpResponseMessage>(msg => msg.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(1), async (exception, _, retryCount, context) =>
                {
                    Console.WriteLine($"Attempt {retryCount} failed for client {client.BaseAddress}. Retrying...");
                });

                try
                {
                    Console.WriteLine($"Attempting to call client: {client.BaseAddress}");
                    // Simulate a call to the client
                    await retryPolicy.ExecuteAsync(async () =>
                    {
                        var response = await client.GetAsync("/api/healthcheck/status");

                        if(response.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"Client {client.BaseAddress} responded successfully.");
                        }

                        return response;
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"All retries failed for client {client.BaseAddress}. Error: {ex.Message}");
                    
                    Console.WriteLine($"Changing to {client}.");
                }
                finally
                {
                    // Get the next client for the next attempt
                    client = _clientPool.GetNextClient();
                }
            }
        }
    }
}