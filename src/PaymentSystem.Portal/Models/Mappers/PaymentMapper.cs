using PaymentSystem.Application;
using PaymentSystem.Contracts.Models;
using PaymentSystem.Domain;
using PaymentSystem.Domain.Models.CreditCards.Commands;

namespace PaymentSystem.Portal.Models.Mappers
{
    public static class PaymentMapper
    {
        public static AddPaymentCommand ToPaymentCommand(this AddPaymentModel model, CreditCardId cardId,
            ISystemClock clock, CurrencyCode currencyCode)
        {
            return new AddPaymentCommand
            {
                Occured = clock.Now,
                AggregateId = cardId,
                Payment = new Payment(PaymentId.NewId(), Money.Create(model.Amount, currencyCode), model.Date)
            };
        }
    }
}