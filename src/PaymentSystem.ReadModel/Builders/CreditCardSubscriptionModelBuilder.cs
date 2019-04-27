using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaymentSystem.Contracts.Models;
using PaymentSystem.Domain.Models.CreditCardSubscriptions.Events;
using PaymentSystem.Domain.Models.Events;

namespace PaymentSystem.ReadModel
{
    public class CreditCardSubscriptionModelBuilder :
        ViewModelUpdateBase<CreditCardSubscriptionModelProjection>,
        INotificationHandler<CreditCardSubscriptionCreated>,
        INotificationHandler<UseNoPaymentFee>,
        INotificationHandler<UseRatePaymentFee>,
        INotificationHandler<UseFixedPaymentFee>
    {
        public CreditCardSubscriptionModelBuilder(IProjectionRepository<CreditCardSubscriptionModelProjection> repo) : base(repo)
        {
        }

        public Task Handle(CreditCardSubscriptionCreated evt, CancellationToken cancellationToken) =>
            Update(CreditCardSubscriptionModelProjection.Id, model =>
            {
                model.Subscriptions.Add(new Subscription
                {
                    Id = evt.AggregateId,
                    Name = evt.Name.ToString()
                });
            });

        public Task Handle(UseNoPaymentFee evt, CancellationToken cancellationToken) =>
            Update(CreditCardSubscriptionModelProjection.Id, model =>
            {
                var subscription = model.Subscriptions.SingleOrDefault(x => x.Id == (Guid)evt.AggregateId);
                if (subscription != null) subscription.Fee = "No Fee";
            });

        public Task Handle(UseRatePaymentFee evt, CancellationToken cancellationToken)
        {
            return Update(CreditCardSubscriptionModelProjection.Id, model =>
            {
                var subscription = model.Subscriptions.SingleOrDefault(x => x.Id == (Guid)evt.AggregateId);
                if (subscription != null) subscription.Fee = $"Rate fee of {evt.FeeRate}";
            });

        }

        public Task Handle(UseFixedPaymentFee evt, CancellationToken cancellationToken)
        {
            return Update(CreditCardSubscriptionModelProjection.Id, model =>
            {
                var subscription = model.Subscriptions.SingleOrDefault(x => x.Id == (Guid)evt.AggregateId);
                if (subscription != null) subscription.Fee = $"Fixed fee of {evt.Fee}";
            });

        }
    }
}