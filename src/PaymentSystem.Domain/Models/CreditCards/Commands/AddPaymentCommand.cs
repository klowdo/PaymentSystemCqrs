using System;

namespace PaymentSystem.Domain.Models.CreditCards.Commands
{
    public class AddPaymentCommand : CreditCardCommand
    {
        public Payment Payment;
        public DateTimeOffset Occured;
    }
}