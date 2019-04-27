using System;

namespace PaymentSystem.Domain.Models.CreditCards.Events
{
    public abstract class CreditCardEvent : Event<CreditCardId>
    {
        protected CreditCardEvent(CreditCardId aggregateId, DateTimeOffset occurred) : base(aggregateId, occurred)
        {
        }
    }
}