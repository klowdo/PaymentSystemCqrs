using System.Collections.Concurrent;
using PaymentSystem.Domain;

namespace PaymentSystem.Infrastructure.Services
{
    public class EventQueue : IEventQueue
    {
        private readonly ConcurrentQueue<Event> Queue = new ConcurrentQueue<Event>();

        public void Enqueue(Event evt)
        {
            Queue.Enqueue(evt);
        }

        public bool TryDequeue(out Event evt)
        {
            return Queue.TryDequeue(out evt);
        }

        public bool IsEmpty => Queue.IsEmpty;
    }
}