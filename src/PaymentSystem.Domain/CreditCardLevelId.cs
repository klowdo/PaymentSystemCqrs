using System;
using System.ComponentModel;

namespace PaymentSystem.Domain
{
    [TypeConverter(typeof(Converter))]
    public struct CreditCardSubscriptionId : IEquatable<CreditCardSubscriptionId>
    {
        private readonly Guid _value;

        public CreditCardSubscriptionId(Guid value)
        {
            _value = value;
        }

        public static CreditCardSubscriptionId Empty => new CreditCardSubscriptionId(Guid.Empty);

        public static CreditCardSubscriptionId NewId()
        {
            return new CreditCardSubscriptionId(Guid.NewGuid());
        }

        public static implicit operator Guid(CreditCardSubscriptionId id)
        {
            return id._value;
        }

        internal class Converter : GuidTypeConverter<CreditCardSubscriptionId>
        {
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public bool Equals(CreditCardSubscriptionId other)
        {
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            return obj is CreditCardSubscriptionId id && Equals(id);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(CreditCardSubscriptionId left, CreditCardSubscriptionId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CreditCardSubscriptionId left, CreditCardSubscriptionId right)
        {
            return !left.Equals(right);
        }

        public static implicit operator CreditCardSubscriptionId(Guid id)
        {
            return new CreditCardSubscriptionId(id);
        }

        public static CreditCardSubscriptionId Parse(string id)
        {
            return new CreditCardSubscriptionId(Guid.Parse(id));
        }
    }
}