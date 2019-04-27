using System;

namespace PaymentSystem.Domain.Models.CreditCardSubscriptions.Events
{
    public class UseRatePaymentFee : CreditCardSubscriptionEvent
    {
        public readonly Rate FeeRate;

        public UseRatePaymentFee(CreditCardSubscriptionId id, DateTimeOffset occured, Rate feeRate) : base(id, occured)
        {
            FeeRate = feeRate;
        }
    }
}