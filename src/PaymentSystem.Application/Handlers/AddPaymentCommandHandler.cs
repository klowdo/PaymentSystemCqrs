using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaymentSystem.Domain;
using PaymentSystem.Domain.Models.CreditCards;
using PaymentSystem.Domain.Models.CreditCards.Commands;
using PaymentSystem.Domain.Models.CreditCardSubscriptions;

namespace PaymentSystem.Application.Handlers
{
    public class AddPaymentCommandHandler : CommandHandler<CreditCard, CreditCardId>, IRequestHandler<AddPaymentCommand>
    {
        private readonly IAggregateRepository<CreditCardSubscription> _subscriptionRepo;

        public AddPaymentCommandHandler(IAggregateRepository<CreditCard> creditCardRepo,
            IAggregateRepository<CreditCardSubscription> subscriptionRepo) : base(creditCardRepo)
        {
            _subscriptionRepo = subscriptionRepo;
        }

        public async Task<Unit> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            await PerformAsync(request, async creditCard =>
            {
                var subscription = await _subscriptionRepo.GetByIdAsync(creditCard.SubscriptionId);
                subscription.AddTransactionTo(creditCard, request.Payment, request.Occured);
            });
            return Unit.Value;
        }
    }
}