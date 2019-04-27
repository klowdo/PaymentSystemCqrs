namespace PaymentSystem.Domain.Models.CreditCardSubscriptions.PaymentFeeStratgies
{
    public class FixedFeeStrategy : IPaymentFeeStrategy
    {
        private readonly Money _flatFee;

        public FixedFeeStrategy(Money flatFee)
        {
            _flatFee = flatFee;
        }

        public Money CalculateFee(Payment payment)
        {
            return _flatFee;
        }
    }
}