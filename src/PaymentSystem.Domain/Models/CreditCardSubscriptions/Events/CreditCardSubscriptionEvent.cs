using System;

namespace PaymentSystem.Domain.Models.CreditCardSubscriptions.Events
{
    public abstract class CreditCardSubscriptionEvent : Event<CreditCardSubscriptionId>
    {
        public CreditCardSubscriptionEvent(CreditCardSubscriptionId aggregateId, DateTimeOffset occurred) : base(
            aggregateId, occurred)
        {
        }
    }
}