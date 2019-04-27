namespace PaymentSystem.Domain
{
    public class RateFeeStrategy : IPaymentFeeStrategy
    {
        public RateFeeStrategy(Rate feeRate) => _feeRate = feeRate;
        private readonly Rate _feeRate;

        public Money CalculateFee(Payment payment) => _feeRate.GetRateOf(payment.Value);
    }
}