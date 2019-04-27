using System;

namespace PaymentSystem.Domain.Models.CreditCards.Events
{
    public class CreditCardTransactionAdded : CreditCardEvent
    {
        public readonly Transaction Transaction;

        public CreditCardTransactionAdded(CreditCardId id, DateTimeOffset occured, Transaction transaction) : base(id,
            occured)
        {
            Transaction = transaction;
        }
    }
}