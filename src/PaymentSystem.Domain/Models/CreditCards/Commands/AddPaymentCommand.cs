using System;

namespace PaymentSystem.Domain.Models.CreditCards.Commands
{
    public class AddPaymentCommand : CreditCardCommand
    {
        public DateTimeOffset Date;
        public Money Amount;
        public PaymentType PaymentType;
    }
}