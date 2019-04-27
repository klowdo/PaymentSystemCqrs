using System;

namespace PaymentSystem.Contracts.Models
{
    public class TransactionModel
    {
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public Guid ReferenceTransactionId { get; set; }
        public string Type { get; set; }
    }
}