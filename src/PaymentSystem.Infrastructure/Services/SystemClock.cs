using System;
using PaymentSystem.Application;

namespace PaymentSystem.Infrastructure
{
    public class SystemClock:ISystemClock
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}