using System;
using System.ComponentModel;

namespace PaymentSystem.Domain
{
    [TypeConverter(typeof(Converter))]
    public struct CreditLevelId : IEquatable<CreditLevelId>
    {
        private readonly Guid _value;

        public CreditLevelId(Guid value) {
            _value = value;
        }

        public static CreditLevelId Empty => new CreditLevelId(Guid.Empty);

        public static CreditLevelId NewId() => new CreditLevelId(Guid.NewGuid());

        public static implicit operator Guid(CreditLevelId id) => id._value;

        internal class Converter : GuidTypeConverter<CreditLevelId> { }

        public override string ToString() => _value.ToString();

        public bool Equals(CreditLevelId other) => _value.Equals(other._value);

        public override bool Equals(object obj) {
            if (obj is null) return false;
            return obj is CreditLevelId id && Equals(id);
        }

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(CreditLevelId left, CreditLevelId right) => left.Equals(right);

        public static bool operator !=(CreditLevelId left, CreditLevelId right) => !left.Equals(right);

        public static CreditLevelId Parse(string id) => new CreditLevelId(Guid.Parse(id));
    }
}