using System;
using System.Linq;
using System.Threading.Tasks;
using PaymentSystem.Application;
using PaymentSystem.Domain;
using PaymentSystem.Infrastructure.Persistance;

namespace PaymentSystem.Infrastructure
{
    public class AggregateRepository<T> : IAggregateRepository<T> where T : class, IAggregate
    {
        private readonly IEventStore _storage;

        public AggregateRepository(IEventStore storage)
        {
            _storage = storage;
        }


        public async Task SaveAsync(T aggregate, long expectedVersion)
        {
            await _storage.SaveEventsAsync(aggregate.Id, aggregate.GetUncommittedChanges().ToArray(), expectedVersion);
            aggregate.CommitChanges();
        }

        public Task<bool> ExistsAsync<TId>(TId id)
        {
            return Task.FromResult(_storage.Exists(id));
        }

        public async Task<T> GetByIdAsync<TId>(TId id)
        {
            var obj = ConstructAggregate(id);
            var e = await _storage.GetEventsForAggregate(id);
            obj.LoadFromHistory(e);
            return obj;
        }

        private static T ConstructAggregate<TId>(TId id)
        {
            return (T) Activator.CreateInstance(typeof(T), id);
        }
    }
}