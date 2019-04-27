using System.Threading.Tasks;
using PaymentSystem.Domain;

namespace PaymentSystem.Infrastructure
{
    public class AggregateRepository<T> : IAggregateRepository<T> where T : class, IAggregate
    {
        private readonly IEventStore _storage;

        public AggregateRepository(IEventStore storage)
        {
            _storage = storage;
        }

        private static T ConstructAggregate(Guid id) => (T)Activator.CreateInstance(typeof(T), id);

        public async Task<T> GetByIdAsync(Guid id)
        {
            var obj = ConstructAggregate(id);
            var e = await _storage.GetEventsForAggregateAsync(id);
            obj.LoadFromHistory(e);
            return obj;
        }

        public Task<T> GetByIdAsync(Guid id, int version) => throw new NotImplementedException();

        public Task<T> GetByIdAsync(Guid id, Guid eventId) => throw new NotImplementedException();

        public async Task SaveAsync(T aggregateRoot, int version, Guid correlationId)
        {
            await _storage.SaveEventsAsync(aggregateRoot.Id, aggregateRoot.GetUncommittedChanges().ToArray(), version);
            aggregateRoot.CommitChanges();
        }
    }
}