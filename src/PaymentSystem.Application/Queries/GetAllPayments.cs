using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaymentSystem.Contracts.Models;
using PaymentSystem.Domain;
using PaymentSystem.ReadModel;
using PaymentSystem.ReadModel.Projections;
using PaymentSystem.ReadModel.Services;

namespace PaymentSystem.Application.Queries
{
    public class GetAllPayments : IRequest<IEnumerable<TransactionModel>>
    {
        public GetAllPayments(CreditCardId cardId)
        {
            CardId = cardId;
        }

        public CreditCardId CardId { get; }
    }

    public class ListPaymentsRequestHandler : IRequestHandler<GetAllPayments, IEnumerable<TransactionModel>>
    {
        private readonly IProjectionRepository<CreditCardTransactionProjection> _repository;

        public ListPaymentsRequestHandler(IProjectionRepository<CreditCardTransactionProjection> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TransactionModel>> Handle(GetAllPayments request,
            CancellationToken cancellationToken)
        {
            var projection = await _repository.GetAsync(request.CardId);
            return projection?.Transactions.AsEnumerable() ?? new List<TransactionModel>();
        }
    }
}