using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaymentSystem.Domain;
using PaymentSystem.Domain.Models.CreditCards;
using PaymentSystem.Domain.Models.CreditCards.Commands;
using PaymentSystem.Domain.Models.CreditCardSubscriptions;

namespace PaymentSystem.Application.Handlers
{
    public class CreateCreditCardHandler :CommandHandler<CreditCard, CreditCardId>, IRequestHandler<CreateCreditCard>
    {
        private readonly IAggregateRepository<CreditCard> _creditCardRepo;
        private readonly IAggregateRepository<CreditCardSubscription> _subscriptionRepo;
        private readonly ISystemClock _clock;

        public CreateCreditCardHandler(IAggregateRepository<CreditCard> creditCardRepo, IAggregateRepository<CreditCardSubscription> subscriptionRepo, ISystemClock clock): base(creditCardRepo)
        {
            _creditCardRepo = creditCardRepo;
            _subscriptionRepo = subscriptionRepo;
            _clock = clock;
        }

        public async Task<Unit> Handle(CreateCreditCard request, CancellationToken cancellationToken)
        {
            if(!await _subscriptionRepo.ExistsAsync(request.CreditCardSubscriptionId))
                throw new InvalidOperationException("Can not create a card with non existing subscription");
            var card = new CreditCard(CreditCardId.NewId(), request.CreditCardSubscriptionId, request.Occured);
            await SaveAsync(card, -1);
            return Unit.Value;
        }
    }
}