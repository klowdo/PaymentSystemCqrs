using System;
using MediatR;

namespace PaymentSystem.Domain
{
    public abstract class Message : IRequest
    {
    }

    public abstract class Event : Message, INotification
    {
        public readonly Guid Id = Guid.NewGuid();
        public readonly DateTimeOffset Occurred;

        protected Event(DateTimeOffset occurred)
        {
            Occurred = occurred;
        }

        public long Version { get; set; } = -1;
    }

    public abstract class Event<TId> : Event
    {
        public readonly TId AggregateId;

        protected Event(TId aggregateId, DateTimeOffset occurred) : base(occurred)
        {
            AggregateId = aggregateId;
        }
    }
}