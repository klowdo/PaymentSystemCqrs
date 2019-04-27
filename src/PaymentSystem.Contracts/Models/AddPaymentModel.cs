using System;

namespace PaymentSystem.Contracts.Models
{
    public class AddPaymentModel
    {
        public DateTimeOffset Date { get; set; }
        public decimal Amount { get; set; }
    }
}