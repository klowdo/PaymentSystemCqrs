using System;
using System.ComponentModel;

namespace PaymentSystem.Domain
{
    [TypeConverter(typeof(Converter))]
    public struct CreditCardId : IEquatable<CreditCardId>
    {
        private readonly Guid _value;

        public CreditCardId(Guid value)
        {
            _value = value;
        }

        public static CreditCardId Empty => new CreditCardId(Guid.Empty);

        public static CreditCardId NewId()
        {
            return new CreditCardId(Guid.NewGuid());
        }

        public static implicit operator Guid(CreditCardId id)
        {
            return id._value;
        }

        internal class Converter : GuidTypeConverter<CreditCardId>
        {
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public bool Equals(CreditCardId other)
        {
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            return obj is CreditCardId id && Equals(id);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(CreditCardId left, CreditCardId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CreditCardId left, CreditCardId right)
        {
            return !left.Equals(right);
        }

        public static CreditCardId Parse(string id)
        {
            return new CreditCardId(Guid.Parse(id));
        }
    }
}