namespace PaymentService.Models
{
    public class Payment
    {
        public DateOnly Date { get; set; }
        public int Id { get; set; }
        public required string Payee { get; set; }
        public decimal Amount { get; set; }

		//SAGA relevante Attribute
		public Guid? SagaId { get; set; }
		public bool IsReserved { get; set; } = false;
	}

	public class PaymentReservationRequest
	{
		public Guid SagaId { get; set; }
		public DateOnly Date { get; set; }
		public int Id { get; set; }
		public required string Payee { get; set; }
		public decimal Amount { get; set; }
	}

	public class PaymentCompensationRequest
	{
		public Guid SagaId { get; set; }
		public int PaymentId { get; set; }
	}
}
