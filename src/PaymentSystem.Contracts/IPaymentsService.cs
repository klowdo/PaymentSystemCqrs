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
        [Post("api/payments/{cardId}/add")]
        Task<HttpResponseMessage> AddPayment(Guid cardId, [Body] AddPaymentModel model);

        [Get("api/payments/{cardId}/transactions")]
        Task<TransactionsModel> Transactions(Guid cardId, [Body] AddPaymentModel model);
    }
}