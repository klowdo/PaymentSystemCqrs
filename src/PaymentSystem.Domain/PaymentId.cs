using System;
using System.ComponentModel;

namespace PaymentSystem.Domain
{
    [TypeConverter(typeof(Converter))]
    public struct PaymentId : IEquatable<PaymentId>
    {
        private readonly Guid _value;

        public PaymentId(Guid value) => _value = value;

        public static PaymentId Empty => new PaymentId(Guid.Empty);

        public static PaymentId NewId() => new PaymentId(Guid.NewGuid());

        public static implicit operator Guid(PaymentId id) => id._value;

        internal class Converter : GuidTypeConverter<PaymentId> { }

        public override string ToString() => _value.ToString();

        public bool Equals(PaymentId other) => _value.Equals(other._value);

        public override bool Equals(object obj) {
            if (obj is null) return false;
            return obj is CreditCardId id && Equals(id);
        }

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(PaymentId left, PaymentId right) => left.Equals(right);

        public static bool operator !=(PaymentId left, PaymentId right) => !left.Equals(right);

        public static PaymentId Parse(string id) => new PaymentId(Guid.Parse(id));
    }
}