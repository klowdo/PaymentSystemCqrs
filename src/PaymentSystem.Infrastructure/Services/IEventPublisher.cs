using System.Threading.Tasks;
using PaymentSystem.Domain;

namespace PaymentSystem.Infrastructure.Services
{
    public interface IEventPublisher
    {
        Task PublishAsync(Event evt);
    }
}