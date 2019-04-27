using System;
using System.Collections.Generic;

namespace PaymentSystem.Domain
{
    public interface IAggregate
    {
        object Id { get; }

        Type IdType { get; }

        long Version { get; }
        void LoadFromHistory(IEnumerable<Event> events);

        IEnumerable<Event> GetUncommittedChanges();

        void CommitChanges();
    }
}