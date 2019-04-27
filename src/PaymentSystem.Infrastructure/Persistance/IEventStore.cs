using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentSystem.Domain;

namespace PaymentSystem.Infrastructure
{
    public interface IEventStore
    {
        Task SaveEventsAsync(object aggregateId, IEnumerable<Event> events, long expectedVersion);
        Task<List<Event>> GetEventsForAggregate(object aggregateId);
        bool Exists<TId>(TId id);
    }
}