using System;

namespace PaymentSystem.Domain
{
    public sealed class Money : IEquatable<Money>
    {
        public readonly CurrencyCode CurrencyCode;
        public readonly decimal Value;

        public Money(decimal value, CurrencyCode currencyCode)
        {
            Value = value;
            CurrencyCode = currencyCode;
        }

        public bool Equals(Money other)
        {
            return Value == other.Value && CurrencyCode == other.CurrencyCode;
        }

        public override bool Equals(object obj)
        {
            if (obj is Money otherMoney)
                return Equals(otherMoney);
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Value.GetHashCode() * 397) ^ (int) CurrencyCode;
            }
        }

        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }


        public static Money operator +(Money money1, Money money2)
        {
            if (money1.CurrencyCode != money2.CurrencyCode)
                throw new InvalidOperationException("Can not use addition on different currencies");
            return new Money(money1.Value + money2.Value, money1.CurrencyCode);
        }

        public static Money operator -(Money money1, Money money2)
        {
            if (money1.CurrencyCode != money2.CurrencyCode)
                throw new InvalidOperationException("Can not use subtraction on different currencies");
            return new Money(money1.Value - money2.Value, money1.CurrencyCode);
        }

        public static Money operator *(Money money1, decimal money2)
        {
            return new Money(money1.Value * money2, money1.CurrencyCode);
        }

        public static Money operator *(Money money1, int money2)
        {
            return new Money(money1.Value * money2, money1.CurrencyCode);
        }

        public static Money operator /(Money money1, int money2)
        {
            return new Money(money1.Value / money2, money1.CurrencyCode);
        }

        public static bool operator ==(Money left, Money right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Money left, Money right)
        {
            return !left.Equals(right);
        }

        public Money RoundOff()
        {
            var newAmount = Math.Round(Value, 0, MidpointRounding.AwayFromZero);
            return Create(newAmount, CurrencyCode);
        }

        public static Money Create(decimal value, CurrencyCode currencyCode)
        {
            return new Money(value, currencyCode);
        }

        public static Money CreateAUD(decimal value)
        {
            return new Money(value, CurrencyCode.AUD);
        }

        public static Money Zero(CurrencyCode currencyCode)
        {
            return new Money(decimal.Zero, currencyCode);
        }

        public override string ToString()
        {
            return $"{Value} {CurrencyCode}";
        }

        public string ToStringWithOutCurrency()
        {
            return Value.ToString();
        }
    }

    public enum CurrencyCode
    {
        AUD,
        SEK,
        UNKNOWN
    }
}