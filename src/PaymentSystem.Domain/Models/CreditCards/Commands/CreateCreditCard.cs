using System;

namespace PaymentSystem.Domain.Models.CreditCards.Commands
{
    public class CreateCreditCard : CreditCardCommand
    {
        public CreditCardId CreditCardId;
        public CreditCardSubscriptionId CreditCardSubscriptionId;
        public DateTimeOffset Occured;

        public CreateCreditCard(CreditCardId creditCardId, CreditCardSubscriptionId creditCardSubscriptionId,
            DateTimeOffset occured)
        {
            CreditCardSubscriptionId = creditCardSubscriptionId;
            CreditCardId = creditCardId;
            Occured = occured;
        }
    }
}