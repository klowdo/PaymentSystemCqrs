using System;

namespace PaymentSystem.Domain.Models.Events
{
    public class UseNoPaymentFee : CreditCardSubscriptionEvent
    {
        public UseNoPaymentFee(CreditCardSubscriptionId id, DateTimeOffset occured) : base(id, occured)
        {
        }
    }
}