using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentSystem.Domain;

namespace PaymentSystem.Infrastructure
{
    public class EventStore : IEventStore
    {
        private readonly IEventQueue _eventQueue;

        private struct EventDescriptor
        {
            public readonly Event EventData;
            public readonly Guid Id;
            public readonly long Version;

            public EventDescriptor(Guid id, Event eventData, long version)
            {
                EventData = eventData;
                Version = version;
                Id = id;
            }
        }

        public EventStore(IEventQueue eventQueue)
        {
            _eventQueue = eventQueue;
        }

        private readonly Dictionary<object, List<EventDescriptor>> _current =
            new Dictionary<object, List<EventDescriptor>>();

        public  Task SaveEventsAsync(object aggregateId, IEnumerable<Event> events, long expectedVersion)
        {
            List<EventDescriptor> eventDescriptors;

            // try to get event descriptors list for given aggregate id
            // otherwise -> create empty dictionary
            if (!_current.TryGetValue(aggregateId, out eventDescriptors))
            {
                eventDescriptors = new List<EventDescriptor>();
                _current.Add(aggregateId, eventDescriptors);
            }
            // check whether latest event version matches current aggregate version
            // otherwise -> throw exception
            else if (eventDescriptors[eventDescriptors.Count - 1].Version != expectedVersion && expectedVersion != -1)
            {
                throw new ConcurrencyException();
            }

            var i = expectedVersion;

            // iterate through current aggregate events increasing version with each processed event
            foreach (var @event in events)
            {
                i++;
                @event.Version = i;

                // push event to the event descriptors list for current aggregate
                eventDescriptors.Add(new EventDescriptor(@event.Id, @event, i));

                // publish current event to the bus for further processing by subscribers
                _eventQueue.Enqueue(@event);
            }
            return Task.CompletedTask;
        }


        public Task<List<Event>> GetEventsForAggregate(object aggregateId)
        {
            List<EventDescriptor> eventDescriptors;

            if (!_current.TryGetValue(aggregateId, out eventDescriptors))
            {
                throw new AggregateNotFoundException();
            }

            return Task.FromResult(eventDescriptors.Select(desc => desc.EventData).ToList());
        }

        public bool Exists<TId>(TId id) => _current.ContainsKey(id);
    }
}