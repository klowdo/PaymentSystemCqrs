using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaymentSystem.Contracts.Models;
using PaymentSystem.Domain.Models.CreditCards;
using PaymentSystem.Domain.Models.CreditCards.Events;

namespace PaymentSystem.ReadModel
{
    public class CreditCardTransactionProjectionBuilder :
        ViewModelUpdateBase<CreditCardTransactionProjection>,
        INotificationHandler<CreditCardCreated>,
        INotificationHandler<CreditCardTransactionAdded>
    {
        public CreditCardTransactionProjectionBuilder(IProjectionRepository<CreditCardTransactionProjection> repo) : base(repo)
        {
        }

        public Task Handle(CreditCardCreated evt, CancellationToken cancellationToken)
        {
            return  Repo.SaveAsync(new CreditCardTransactionProjection
            {
                ProjectionId = evt.AggregateId
            });
        }

        public Task Handle(CreditCardTransactionAdded evt, CancellationToken cancellationToken)
        {
            return Update(evt.AggregateId, model =>
            {
                if (evt.Transaction.Type == TransactionType.Fee && evt.Transaction.ReferenceId != null)
                {
                    model.Transactions.SingleOrDefault(x => x.Id == evt.Transaction.ReferenceId)?
                        .FeeTransactions.Add(new TransactionModel
                        {
                            Amount = evt.Transaction.Value,
                            Created = evt.Transaction.Created,
                            CurrencyCode = evt.Transaction.Value.CurrencyCode.ToString("G"),
                            Id = evt.Transaction.Id,
                            Type = TransactionType.Fee.ToString("G")
                        });
                }
                else
                {
                    model.Transactions.Add(new TransactionModel
                    {
                        Amount = evt.Transaction.Value,
                        Created = evt.Transaction.Created,
                        CurrencyCode = evt.Transaction.Value.CurrencyCode.ToString("G"),
                        Id = evt.Transaction.Id,
                        Type = TransactionType.Payment.ToString("G")
                    });
                }
            });
        }
    }
}