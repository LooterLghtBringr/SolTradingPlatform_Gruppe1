using Microsoft.AspNetCore.Mvc;
using PaymentService.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        private static readonly List<Payment> Payments = new List<Payment>
        {
            new Payment { Id = 1, Payee = "Alice", Amount = 150.00m, Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)) },
            new Payment { Id = 2, Payee = "Bob", Amount = 200.50m, Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)) }
        };

        public PaymentController(IHttpClientFactory httpClientFactory, ILogger<PaymentController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
        }

        [HttpGet("GetPayment")]
        public ActionResult<IEnumerable<Payment>> GetPayments()
        {
            return Ok(Payments);
        }

        [HttpPost("AddPayment")]
        public ActionResult<Payment> CreatePayment([FromBody] Payment payment)
        {
            payment.Id = Payments.Count + 1;
            Payments.Add(payment);

            SendOrderCreatedWebhookAsync(payment).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    _logger.LogError("Failed to send webhook: {0}", task.Exception?.Message);
                }
            });

            return CreatedAtAction(nameof(GetPayments), new { id = payment.Id }, payment);
        }

        private async Task SendOrderCreatedWebhookAsync(Payment payment)
        {
            var webhookUrl = "https://localhost:7294/api/webhook/payment-created";
            var payload = new
            {
                Payee = payment.Payee,
                Date = payment.Date,
                Amount = payment.Amount
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(webhookUrl, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
