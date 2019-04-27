using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaymentSystem.Contracts.Models;
using PaymentSystem.Domain.Models.CreditCards;
using PaymentSystem.Domain.Models.CreditCards.Events;

namespace PaymentSystem.ReadModel
{
    public class AllCreditCardsProjectionsBuilder :
        ViewModelUpdateBase<AllCreditCardsProjections>,
        INotificationHandler<CreditCardCreated>
    {
        public AllCreditCardsProjectionsBuilder(IProjectionRepository<AllCreditCardsProjections> repo) : base(repo)
        {
        }

        public Task Handle(CreditCardCreated evt, CancellationToken cancellationToken)
        {
            return Update(AllCreditCardsProjections.Id, model =>
            {
                model.Cards.Add(new CreditCardModel
                {
                    Id = evt.AggregateId,
                    SubscriptionId = evt.CreditCardSubscription
                });
            });
        }
    }
}