using System.Threading.Tasks;
using PaymentSystem.Domain;

namespace PaymentSystem.Infrastructure
{
    public interface IEventPublisher
    {
       Task PublishAsync(Event evt);
    }
}