using System;

namespace PaymentSystem.Domain
{
    public class Rate : IEquatable<Rate>
    {
        private readonly decimal _maximumRate = decimal.One;
        private readonly decimal _minimumRate = decimal.Zero;
        private readonly decimal _rate;

        private Rate(decimal rate)
        {
            if (rate > _maximumRate || rate < _minimumRate)
                throw new ArgumentException(nameof(rate), $"must be between {_minimumRate} and {_maximumRate}");
            _rate = rate;
        }

        public bool Equals(Rate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _maximumRate == other._maximumRate && _minimumRate == other._minimumRate && _rate == other._rate;
        }

        public Money GetRateOf(Money amount)
        {
            return amount * _rate;
        }

        public static Rate Create(decimal rate)
        {
            return new Rate(rate);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Rate) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _maximumRate.GetHashCode();
                hashCode = (hashCode * 397) ^ _minimumRate.GetHashCode();
                hashCode = (hashCode * 397) ^ _rate.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return _rate.ToString("G");
        }
    }
}