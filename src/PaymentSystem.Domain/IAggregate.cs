using System;
using System.Collections.Generic;

namespace PaymentSystem.Domain
{
    public interface IAggregate
    {
        void LoadFromHistory(IEnumerable<Event> events);

        IEnumerable<Event> GetUncommittedChanges();

        void CommitChanges();

        object Id { get; }

        Type IdType { get; }

        long Version { get; }

    }
}