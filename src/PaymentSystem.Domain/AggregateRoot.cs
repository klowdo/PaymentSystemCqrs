using System;
using System.Collections.Generic;

namespace PaymentSystem.Domain
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregate
    {
        private readonly List<Event> _changes = new List<Event>();

        protected AggregateRoot(TId id) : base(id)
        {
        }

        public void CommitChanges()
        {
            _changes.Clear();
        }

        object IAggregate.Id => Id;

        Type IAggregate.IdType => typeof(TId);

        public long Version { get; protected set; } = -1;

        public void LoadFromHistory(IEnumerable<Event> events)
        {
            foreach (var e in events) ApplyChange(e, false);
        }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _changes;
        }


        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        // push atomic aggregate changes to local history for further processing (EventStore.SaveEvents)
        private void ApplyChange(Event @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);
            if (isNew) _changes.Add(@event);
        }
    }
}