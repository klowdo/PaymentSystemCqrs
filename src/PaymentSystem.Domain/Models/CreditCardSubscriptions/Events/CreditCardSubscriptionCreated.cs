using System;

namespace PaymentSystem.Domain.Models.CreditCardSubscriptions.Events
{
    public class CreditCardSubscriptionCreated : CreditCardSubscriptionEvent
    {
        public readonly CreditCardSubscriptionName Name;

        public CreditCardSubscriptionCreated(CreditCardSubscriptionId aggregateId, DateTimeOffset occurred,
            CreditCardSubscriptionName name) : base(aggregateId, occurred)
        {
            Name = name;
        }
    }
}