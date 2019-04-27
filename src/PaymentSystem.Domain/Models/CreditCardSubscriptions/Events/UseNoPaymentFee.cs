using System;

namespace PaymentSystem.Domain.Models.CreditCardSubscriptions.Events
{
    public class UseNoPaymentFee : CreditCardSubscriptionEvent
    {
        public UseNoPaymentFee(CreditCardSubscriptionId id, DateTimeOffset occured) : base(id, occured)
        {
        }
    }
}