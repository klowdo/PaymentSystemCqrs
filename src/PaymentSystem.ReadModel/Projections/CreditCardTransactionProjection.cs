using System;
using System.Collections.Generic;
using PaymentSystem.Contracts.Models;

namespace PaymentSystem.ReadModel
{
    public class CreditCardTransactionProjection : IProjection
    {
        public Guid ProjectionId { get; set; }
        public IList<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();
    }
}