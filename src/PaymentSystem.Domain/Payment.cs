using System;

namespace PaymentSystem.Domain
{
    public class Payment : Entity<PaymentId>
    {
        public Payment(PaymentId id, Money value, DateTimeOffset created) : base(id)
        {
            Value = value;
            Created = created;
        }

        public Money Value { get; }
        public DateTimeOffset Created { get; }
    }
}