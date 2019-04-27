using System;

namespace PaymentSystem.Domain.Models.CreditCards.Events
{
    public class CreditCardCreated : CreditCardEvent
    {
        public readonly CreditCardSubscriptionId CreditCardSubscription;

        public CreditCardCreated(CreditCardId id, CreditCardSubscriptionId creditCardSubscription, DateTimeOffset occured) : base(id, occured)
        {
            CreditCardSubscription = creditCardSubscription;
        }
    }
}