using System;

namespace PaymentSystem.Domain.Models.CreditCardSubscriptions.Events
{
    public class UseFixedPaymentFee : CreditCardSubscriptionEvent
    {
        public readonly Money Fee;

        public UseFixedPaymentFee(CreditCardSubscriptionId id, DateTimeOffset occured, Money fee) : base(id, occured)
        {
            Fee = fee;
        }
    }
}