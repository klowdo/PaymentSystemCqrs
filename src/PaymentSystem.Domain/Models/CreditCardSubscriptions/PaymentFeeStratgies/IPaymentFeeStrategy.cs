namespace PaymentSystem.Domain
{
    public interface IPaymentFeeStrategy
    {
        Money CalculateFee(Payment payment);
    }
    public class BronzeFeeStrategy:IPaymentFeeStrategy
    {
        private readonly Money _flatFee = Money.CreateAUD(1.2m);
        public Money CalculateFee(Payment payment) => _flatFee;
    }

    public class SilverFeeStrategy : IPaymentFeeStrategy
    {
        private readonly Rate _feeRate = Rate.Create(0.2m);

        public Money CalculateFee(Payment payment)
        {
            return _feeRate.GetRateOf(payment.Value);
        }
    }
    public class GoldFeeStrategy : IPaymentFeeStrategy
    {
        private readonly Rate _feeRate = Rate.Create(0.2m);

        public Money CalculateFee(Payment payment)
        {
            return Money.Zero(payment.Value.CurrencyCode);
        }
    }
}