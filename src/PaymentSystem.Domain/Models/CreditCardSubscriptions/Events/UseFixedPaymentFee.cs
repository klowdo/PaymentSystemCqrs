using System;

namespace PaymentSystem.Domain.Models.Events
{
    public class UseFixedPaymentFee : CreditCardSubscriptionEvent
    {
        public readonly DateTimeOffset Occured;
        public readonly Money Fee;

        public UseFixedPaymentFee(CreditCardSubscriptionId id, DateTimeOffset occured, Money fee) : base(id)
        {
            Occured = occured;
            Fee = fee;
        }
    }
}