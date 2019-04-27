using System;

namespace PaymentSystem.Domain
{
    [Serializable]
    public abstract class Entity<TId>
    {
        protected Entity(TId id)
        {
            Id = id;
        }

        public TId Id { get; }

        public override bool Equals(object obj)
        {
            return obj is Entity<TId> other && Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}