namespace PaymentService.Models
{
    public class Payment
    {
        public DateOnly Date { get; set; }
        public int Id { get; set; }
        public required string Payee { get; set; }
        public decimal Amount { get; set; }
    }
}
