using System;
using PaymentSystem.Domain.Models.CreditCards;

namespace PaymentSystem.Domain.Models
{
    public class CreditCardSubscription:Entity<CreditCardSubscriptionId>
    {
        private readonly CreditCardSubscriptionName _name;
        private readonly IPaymentFeeStrategy _paymentFeeStrategy;

        public CreditCardSubscription(CreditCardSubscriptionId id, CreditCardSubscriptionName name, IPaymentFeeStrategy paymentFeeStrategy) : base(id)
        {
            _name = name;
            _paymentFeeStrategy = paymentFeeStrategy;
        }

        public void AddTransactionTo(CreditCard creditCard, Payment payment, DateTimeOffset now)
        {
            creditCard.Add(Transaction.CreatePayment(payment));
            var fee = _paymentFeeStrategy.CalculateFee(payment);
            if (fee != Money.Zero(payment.Value.CurrencyCode))
                creditCard.Add(Transaction.CreateFee(fee, now));
        }
    }
}