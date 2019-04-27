using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PaymentSystem.Contracts.Models;
using Refit;

namespace PaymentSystem.Contracts
{
    public interface IPaymentsService
    {
        [Post("/api/creditcard/create-card")]
        Task<CreditCardCreatedResponse> CreateCard([Body] CreateCreditCardModel model);

        [Post("/api/creditcard/{cardId}/add-payment")]
        Task<HttpResponseMessage> AddPayment(Guid cardId, [Body] AddPaymentModel model);

        [Get("/api/creditcard/{cardId}/transactions")]
        Task<IEnumerable<TransactionModel>> GetTransactions(Guid cardId);

        [Get("/api/creditcard/subscriptions")]
        Task<IEnumerable<Subscription>> GetSubscriptions();

        [Get("/api/creditcard/all")]
        Task<IEnumerable<CreditCardModel>> GetAllCards();
    }
}