using System;

namespace PaymentSystem.Domain.Models.Events
{
    public abstract class CreditCardSubscriptionEvent:Event<CreditCardSubscriptionId>
    {
        public CreditCardSubscriptionEvent(CreditCardSubscriptionId aggregateId, DateTimeOffset occurred) : base(aggregateId, occurred)
        {
        }
    }
}