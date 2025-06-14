using System.Xml.Serialization;

namespace PaymentService.Models
{
    [XmlRoot("Payment")]
    public class Payment
    {
        [XmlIgnore]
        public DateOnly Date { get; set; }

        [XmlElement(ElementName = "OnlyTheDate", DataType = "date")]
        internal string OnlyTheDataSurrogate
        {
            get { return Date.ToString("yyyy-MM-dd"); }
            set { Date = DateOnly.Parse(value); }
        }

        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Payee")]
        public string Payee { get; set; }

        [XmlElement("Amount")]
        public decimal Amount { get; set; }

        //SAGA relevante Attribute
        [XmlElement("SagaId")]
        public Guid? SagaId { get; set; }

        [XmlElement("IsReserved")]
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
