namespace PaymentSystem.Domain.Models.CreditCardSubscriptions.PaymentFeeStratgies
{
    public class RateFeeStrategy : IPaymentFeeStrategy
    {
        private readonly Rate _feeRate;

        public RateFeeStrategy(Rate feeRate)
        {
            _feeRate = feeRate;
        }

        public Money CalculateFee(Payment payment)
        {
            return _feeRate.GetRateOf(payment.Value);
        }
    }
}