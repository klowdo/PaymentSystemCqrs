namespace PaymentSystem.Domain
{
    public class FixedFeeStrategy:IPaymentFeeStrategy
    {
        public FixedFeeStrategy(Money flatFee) => _flatFee = flatFee;
        private readonly Money _flatFee;
        public Money CalculateFee(Payment payment) => _flatFee;
    }
}