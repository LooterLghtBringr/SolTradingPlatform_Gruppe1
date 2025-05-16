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

		[HttpPost("ReservePayment")]
        public async Task<ActionResult> ReservePayment([FromBody] PaymentReservationRequest request)
		{
            try
            {
				if (request.Amount <= 0)
					return BadRequest("Invalid amount");

                var payment = new Payment
                {
					Id = Payments.Count + 1,
					Date = request.Date,
                    Payee = request.Payee,
                    Amount = request.Amount,
                    SagaId = request.SagaId,
                    IsReserved = true,
                };

				Payments.Add(payment);
				_logger.LogInformation($"SAGA {request.SagaId}: Payment reserved (ID: {payment.Id})");

				await SendOrderCreatedWebhookAsync(payment).ContinueWith(task =>
				{
					if (task.IsFaulted)
					{
						_logger.LogError("Failed to send webhook: {0}", task.Exception?.Message);
					}
				});

				return Ok(payment);
			}
            catch (Exception ex)
            {
				_logger.LogError(ex, $"SAGA {request.SagaId}: Payment reservation failed");
				return StatusCode(500, "Payment reservation failed");
			}
		}

		[HttpPost("CompensatePayment")]
		public async Task<ActionResult> CompensatePayment([FromBody] PaymentCompensationRequest request)
		{
			var payment = Payments.FirstOrDefault(p => p.Id == request.PaymentId && p.SagaId == request.SagaId);
			if (payment == null)
			{
				_logger.LogWarning($"SAGA {request.SagaId}: Payment {request.PaymentId} not found for compensation");
				return NotFound();
			}

			Payments.Remove(payment);
			_logger.LogInformation($"SAGA {request.SagaId}: Payment {payment.Id} compensated");

			await SendOrderCreatedWebhookAsync(payment).ContinueWith(task =>
			{
				if (task.IsFaulted)
				{
					_logger.LogError("Failed to send webhook: {0}", task.Exception?.Message);
				}
			});

			return Ok(payment);
		}
	}
}
