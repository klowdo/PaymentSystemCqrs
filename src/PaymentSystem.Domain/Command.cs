using System;

namespace PaymentSystem.Domain
{
    public class Command : Message
    {
        public Guid CorrelationId = Guid.NewGuid();
        public long? Version;
    }

    public class Command<TId> : Command
    {
        public TId AggregateId;
    }
}