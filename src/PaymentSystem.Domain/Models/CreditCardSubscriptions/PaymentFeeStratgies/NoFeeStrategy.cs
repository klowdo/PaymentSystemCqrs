namespace PaymentSystem.Domain
{
    public class NoFeeStrategy : IPaymentFeeStrategy
    {
        public Money CalculateFee(Payment payment) => Money.Zero(payment.Value.CurrencyCode);
    }
}