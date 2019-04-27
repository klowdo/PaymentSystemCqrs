using System;

namespace PaymentSystem.Domain.Models.Events
{
    public class UseRatePaymentFee : Event
    {
        public readonly Rate FeeRate;

        public UseRatePaymentFee(CreditCardSubscriptionId id, DateTimeOffset occured, Rate feeRate) : base(id, occured)
        {
            FeeRate = feeRate;
        }
    }
}