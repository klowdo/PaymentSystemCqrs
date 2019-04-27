using System;

namespace PaymentSystem.Application
{
    public interface ISystemClock
    {
        DateTimeOffset Now { get; }
    }
}