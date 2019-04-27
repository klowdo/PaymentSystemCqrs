namespace PaymentSystem.Domain.Models.CreditCardSubscriptions.PaymentFeeStratgies
{
    public interface IPaymentFeeStrategy
    {
        Money CalculateFee(Payment payment);
    }
}