using System;
using System.ComponentModel;

namespace PaymentSystem.Domain.Models.CreditCardSubscriptions
{
    [TypeConverter(typeof(Converter))]
    public class CreditCardSubscriptionName : IEquatable<CreditCardSubscriptionName>
    {
        public CreditCardSubscriptionName(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public bool Equals(CreditCardSubscriptionName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CreditCardSubscriptionName) obj);
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }

        public class Converter : StringTypeConverter<CreditCardSubscriptionName>
        {
        }
    }
}