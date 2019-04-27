using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaymentSystem.Contracts.Models;
using PaymentSystem.ReadModel;
using PaymentSystem.ReadModel.Projections;
using PaymentSystem.ReadModel.Services;

namespace PaymentSystem.Application.Queries
{
    public class GetAllSubscriptions : IRequest<IEnumerable<Subscription>>
    {
    }

    public class GetAllSubscriptionsHandler : IRequestHandler<GetAllSubscriptions, IEnumerable<Subscription>>
    {
        private readonly IProjectionRepository<CreditCardSubscriptionModelProjection> _repository;

        public GetAllSubscriptionsHandler(IProjectionRepository<CreditCardSubscriptionModelProjection> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Subscription>> Handle(GetAllSubscriptions request,
            CancellationToken cancellationToken)
        {
            var projections = await _repository.GetAsync(CreditCardSubscriptionModelProjection.Id);
            return projections.Subscriptions;
        }
    }
}