using System;

namespace PaymentSystem.Contracts.Models
{
    public class CreateCreditCardModel
    {
        public Guid CreditCardSubscriptionId { get; set; }
    }
}