using System.Collections.Generic;
using PaymentSystem.Domain;

namespace PaymentSystem.Infrastructure
{
    public interface IEventQueue
    {
        void Enqueue(Event evt);
        bool TryDequeue(out Event evt);
        bool IsEmpty { get; }
    }
}