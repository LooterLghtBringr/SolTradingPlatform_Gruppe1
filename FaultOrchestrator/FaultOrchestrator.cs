
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


        /// <summary>
        /// Starts the fault orchestration process, performing health checks on service clients
        /// using a retry policy and round-robin client selection.
        /// </summary>
        /// <param name="numberOfRounds">
        /// The number of rounds to perform health checks. (Currently unused; loop is infinite.)
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// </returns>
        public async Task Start()
        {
            var client = _clientPool.GetNextClient();

            while (true)
            {
                // Define a retry policy that handles exceptions and HTTP 500 responses.
                var retryPolicy = Policy
                        .Handle<Exception>()
                        .OrResult<HttpResponseMessage>(msg => msg.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(1), async (exception, _, retryCount, context) =>
                {
                    Console.WriteLine($"Attempt {retryCount} failed for client {client.BaseAddress}. Retrying...");

                    // Switch to the next client after the last retry attempt.
                    if (retryCount == 3)
                    {
                        client = _clientPool.GetNextClient();
                        Console.WriteLine($"Switching to next client: {client.BaseAddress}");
                    }
                });

                try
                {
                    Console.WriteLine($"Attempting to call client: {client.BaseAddress}");
                    // Simulate a call to the client using the retry policy.
                    await retryPolicy.ExecuteAsync(async () =>
                    {
                        var response = await client.GetAsync("/api/healthcheck/status");

                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"Client {client.BaseAddress} responded successfully.");
                        }

                        return response;
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"All retries failed for client {client.BaseAddress}. Error: {ex.Message}");
                }
            }
        }
    }
}