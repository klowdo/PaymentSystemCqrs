using System;
using System.Collections.Generic;
using PaymentSystem.Domain.Models.CreditCards.Events;

namespace PaymentSystem.Domain.Models.CreditCards
{
    public class CreditCard : AggregateRoot<CreditCardId>
    {
        private readonly IList<Transaction> _transactions = new List<Transaction>();

        public CreditCard(CreditCardId id) : base(id)
        {
        }

        public CreditCard(CreditCardId id, CreditCardSubscriptionId creditCardSubscription,
            DateTimeOffset occured) : base(id)
        {
            ApplyChange(new CreditCardCreated(id, creditCardSubscription, occured));
        }

        public CreditCardSubscriptionId SubscriptionId { get; private set; }


        public void AddTransaction(Transaction transaction, DateTimeOffset occured)
        {
            ApplyChange(new CreditCardTransactionAdded(Id, occured, transaction));
        }

        public void Apply(CreditCardTransactionAdded evt)
        {
            _transactions.Add(evt.Transaction);
        }

        public void Apply(CreditCardCreated evt)
        {
            SubscriptionId = evt.CreditCardSubscription;
        }
    }
}