using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaymentSystem.Contracts.Models;
using PaymentSystem.Domain;
using PaymentSystem.ReadModel;

namespace PaymentSystem.Application.Queries
{
    public class GetAllPayments: IRequest<TransactionsModel>
    {
        public CreditCardId CardId { get; set; }
    }
    public class ListPaymentsRequestHandler: IRequestHandler<GetAllPayments,TransactionsModel>
    {
        private readonly IProjectionRepository<CreditCardTransactionProjection> _repository;

        public ListPaymentsRequestHandler(IProjectionRepository<CreditCardTransactionProjection> repository)
        {
            _repository = repository;
        }
        public async Task<TransactionsModel> Handle(GetAllPayments request, CancellationToken cancellationToken)
        {
            var projection = await _repository.GetAsync(request.CardId);
            return new TransactionsModel
            {
                Transactions = projection.Transactions
            };
        }
    }
}