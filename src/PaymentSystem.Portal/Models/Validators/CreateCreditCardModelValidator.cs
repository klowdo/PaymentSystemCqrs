using FluentValidation;
using PaymentSystem.Application;
using PaymentSystem.Contracts.Models;
using PaymentSystem.Domain;
using PaymentSystem.Domain.Models.CreditCardSubscriptions;

namespace PaymentSystem.Portal.Models.Validators
{
    public class CreateCreditCardModelValidator : AbstractValidator<CreateCreditCardModel>
    {
        public CreateCreditCardModelValidator(IAggregateRepository<CreditCardSubscription> repo)
        {
            RuleFor(x => x.CreditCardSubscriptionId)
                .MustAsync((x, token) => repo.ExistsAsync(new CreditCardSubscriptionId(x)))
                .WithMessage("Subscription does not exist");
        }
    }
}