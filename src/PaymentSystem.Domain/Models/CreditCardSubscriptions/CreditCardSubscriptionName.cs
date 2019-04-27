using System;
using System.ComponentModel;

namespace PaymentSystem.Domain.Models
{
    [TypeConverter(typeof(Converter))]
    public class CreditCardSubscriptionName : IEquatable<CreditCardSubscriptionName>
    {
        public string Name { get; }

        public CreditCardSubscriptionName(string name)
        {
            Name = name;
        }

        public bool Equals(CreditCardSubscriptionName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }
        public class Converter: StringTypeConverter<CreditCardSubscriptionName> {}

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CreditCardSubscriptionName) obj);
        }
         
        public override string ToString() => Name;

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}