using System.Threading.Tasks;
using PaymentSystem.Domain;

namespace PaymentSystem.Application
{
    public interface IAggregateRepository<T> where T : IAggregate
    {
        Task SaveAsync(T aggregate, long expectedVersion);
        Task<bool> ExistsAsync<TId>(TId id);
        Task<T> GetByIdAsync<TId>(TId id);
    }
}