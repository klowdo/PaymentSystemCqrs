using PaymentSystem.Domain;

namespace PaymentSystem.Infrastructure.Services
{
    public interface IEventQueue
    {
        bool IsEmpty { get; }
        void Enqueue(Event evt);
        bool TryDequeue(out Event evt);
    }
}