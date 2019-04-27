using System;
using PaymentSystem.Application;

namespace PaymentSystem.Infrastructure.Services
{
    public class SystemClock : ISystemClock
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}