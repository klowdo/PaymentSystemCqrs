using System;

namespace PaymentSystem.Contracts.Models
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Fee { get; set; }
    }
}