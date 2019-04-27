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
    public class GetAllCards : IRequest<IEnumerable<CreditCardModel>>
    {
    }

    public class GetAllCardsHandler : IRequestHandler<GetAllCards, IEnumerable<CreditCardModel>>
    {
        private readonly IProjectionRepository<AllCreditCardsProjections> _repository;

        public GetAllCardsHandler(IProjectionRepository<AllCreditCardsProjections> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CreditCardModel>> Handle(GetAllCards request, CancellationToken cancellationToken)
        {
            var projections = await _repository.GetAsync(AllCreditCardsProjections.Id);
            return projections?.Cards ?? new List<CreditCardModel>();
        }
    }
}