using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymentSystem.Contracts.Models
{
    public class TransactionModel
    {
        public IList<TransactionModel> FeeTransactions = new List<TransactionModel>();
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string Type { get; set; }
        public decimal Total => Amount + FeeTransactions.Sum(x => x.Amount);
    }
}