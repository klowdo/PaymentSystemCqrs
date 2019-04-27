using System;

namespace PaymentSystem.Contracts.Models
{
    public class CreditCardModel
    {
        public Guid Id { get; set; }
        public Guid SubscriptionId { get; set; }
    }
}