using System;
using System.ComponentModel;

namespace PaymentSystem.Domain.Models.CreditCards
{
    [TypeConverter(typeof(Converter))]
    public struct TransactionId : IEquatable<TransactionId>
    {
        private readonly Guid _value;

        public TransactionId(Guid value)
        {
            _value = value;
        }

        public static TransactionId Empty => new TransactionId(Guid.Empty);

        public static TransactionId NewId()
        {
            return new TransactionId(Guid.NewGuid());
        }

        public static implicit operator Guid(TransactionId id)
        {
            return id._value;
        }

        internal class Converter : GuidTypeConverter<TransactionId>
        {
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public bool Equals(TransactionId other)
        {
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            return obj is TransactionId id && Equals(id);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(TransactionId left, TransactionId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TransactionId left, TransactionId right)
        {
            return !left.Equals(right);
        }

        public static TransactionId Parse(string id)
        {
            return new TransactionId(Guid.Parse(id));
        }
    }
}