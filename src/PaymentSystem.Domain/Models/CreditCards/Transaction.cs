using System;

namespace PaymentSystem.Domain.Models.CreditCards
{
    public class Transaction : Entity<TransactionId>
    {
        public readonly DateTimeOffset Created;
        public readonly TransactionId? ReferenceId;
        public readonly TransactionType Type;
        public readonly Money Value;

        private Transaction(TransactionId id, Money value, TransactionType type, DateTimeOffset created) : base(id)
        {
            Created = created;
            Value = value;
            Type = type;
        }

        private Transaction(TransactionId id, Money value, TransactionType type, TransactionId referenceId,
            DateTimeOffset created) : base(id)
        {
            Created = created;
            Value = value;
            Type = type;
            ReferenceId = referenceId;
        }

        public static Transaction CreatePayment(Payment payment)
        {
            return new Transaction(TransactionId.NewId(), payment.Value, TransactionType.Payment, payment.Created);
        }

        public static Transaction CreateFee(Money value, TransactionId referenceId, DateTimeOffset created)
        {
            return new Transaction(TransactionId.NewId(), value, TransactionType.Fee, referenceId, created);
        }
    }

    public enum TransactionType
    {
        Payment,
        Fee
    }
}