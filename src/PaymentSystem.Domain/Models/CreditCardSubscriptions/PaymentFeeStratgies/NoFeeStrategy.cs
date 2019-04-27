namespace PaymentSystem.Domain.Models.CreditCardSubscriptions.PaymentFeeStratgies
{
    public class NoFeeStrategy : IPaymentFeeStrategy
    {
        public Money CalculateFee(Payment payment)
        {
            return Money.Zero(payment.Value.CurrencyCode);
        }
    }
}