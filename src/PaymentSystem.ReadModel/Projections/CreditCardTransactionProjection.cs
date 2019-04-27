using System;
using System.Collections.Generic;
using PaymentSystem.Contracts.Models;

namespace PaymentSystem.ReadModel.Projections
{
    public class CreditCardTransactionProjection : IProjection
    {
        public IList<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();
        public Guid ProjectionId { get; set; }
    }
}