using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/webhook")]
public class WebhookController : ControllerBase
{
    private static decimal _totalBankAccountAmount = 0;

    [HttpPost("payment-created")]
    public IActionResult PaymentCreated([FromBody] PaymentDto payment)
    {
        _totalBankAccountAmount += payment.Amount;

        Console.WriteLine($"Neue Zahlung empfangen: {payment.Amount} Euro wurden dem Konto gutgeschrieben, Kunde: {payment.Payee}, Datum: {payment.Date}");

        return Ok();
    }

    [HttpGet("GetTotal")]
    public ActionResult<string> GetPayments()
    {
        return Ok($"{_totalBankAccountAmount} Euro");
    }
}

public class PaymentDto
{
    public required string Payee { get; set; }
    public DateOnly Date { get; set; }
    public decimal Amount { get; set; }
}

