using System;
using PaymentSystem.Domain.Models.CreditCards;
using PaymentSystem.Domain.Models.CreditCardSubscriptions.Events;
using PaymentSystem.Domain.Models.CreditCardSubscriptions.PaymentFeeStratgies;

namespace PaymentSystem.Domain.Models.CreditCardSubscriptions
{
    public class CreditCardSubscription : AggregateRoot<CreditCardSubscriptionId>
    {
        private IPaymentFeeStrategy _paymentFeeStrategy;

        public CreditCardSubscription(CreditCardSubscriptionId id) : base(id)
        {
        }

        public CreditCardSubscription(CreditCardSubscriptionId id, DateTimeOffset occured,
            CreditCardSubscriptionName name) : base(id)
        {
            ApplyChange(new CreditCardSubscriptionCreated(Id, occured, name));
        }

        public bool Active => _paymentFeeStrategy != null;


        public void UseFixedPaymentFee(DateTimeOffset occured, Money fee)
        {
            ApplyChange(new UseFixedPaymentFee(Id, occured, fee));
        }

        public void UseRatePaymentFee(DateTimeOffset occured, Rate feeRate)
        {
            ApplyChange(new UseRatePaymentFee(Id, occured, feeRate));
        }

        public void UseNoPaymentFee(DateTimeOffset occured)
        {
            ApplyChange(new UseNoPaymentFee(Id, occured));
        }

        public void AddTransactionTo(CreditCard creditCard, Payment payment, DateTimeOffset now)
        {
            if (!Active) throw new InvalidOperationException("Subscription not active");
            var transaction = Transaction.CreatePayment(payment);
            creditCard.AddTransaction(transaction, now);
            var fee = _paymentFeeStrategy.CalculateFee(payment);
            if (fee != Money.Zero(payment.Value.CurrencyCode))
                creditCard.AddTransaction(Transaction.CreateFee(fee, transaction.Id, now), now);
        }

        public void Apply(UseFixedPaymentFee evt)
        {
            _paymentFeeStrategy = new FixedFeeStrategy(evt.Fee);
        }

        public void Apply(UseRatePaymentFee evt)
        {
            _paymentFeeStrategy = new RateFeeStrategy(evt.FeeRate);
        }

        public void Apply(UseNoPaymentFee evt)
        {
            _paymentFeeStrategy = new NoFeeStrategy();
        }
    }
}