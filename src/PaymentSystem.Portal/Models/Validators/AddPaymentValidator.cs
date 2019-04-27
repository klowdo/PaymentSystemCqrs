using FluentValidation;
using PaymentSystem.Application;
using PaymentSystem.Contracts.Models;

namespace PaymentSystem.Portal.Models.Validators
{
    public class AddPaymentValidator : AbstractValidator<AddPaymentModel>
    {
        public AddPaymentValidator(ISystemClock systemClock)
        {
            RuleFor(x => x.Date).LessThanOrEqualTo(x => systemClock.Now);
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}