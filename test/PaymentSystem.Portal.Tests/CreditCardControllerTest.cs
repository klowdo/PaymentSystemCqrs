using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using PaymentSystem.Contracts;
using PaymentSystem.Contracts.Models;
using PaymentSystem.Domain;
using PaymentSystem.Portal.TempData;
using Refit;

namespace PaymentSystem.Portal.Tests
{
    public class CreditCardControllerTest : PaymentSystemWebApplicationFactory<Startup>
    {
        private Guid _cardId;
        private IPaymentsService _service;

        [SetUp]
        public async Task SetUp()
        {
            _service = RestService.For<IPaymentsService>(CreateClient());
            var response = await _service.CreateCard(new CreateCreditCardModel
            {
                CreditCardSubscriptionId = DefaultCreditCardSubSubscriptions.GoldId
            });
            _cardId = response.CreditCardId;
        }

        [Test]
        public async Task When_AddPayment_Returns_Ok()
        {
            var response = await _service.AddPayment(_cardId, new AddPaymentModel
            {
                Amount = 200,
                Date = DateTimeOffset.Now
            });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task When_Card_added_exists_in_All_card_List()
        {
            await Task.Delay(300);
            var result = await _service.GetAllCards();
            Assert.That(result.Select(x => x.Id), Contains.Item(_cardId));
        }

        [Test]
        public async Task When_Payment_added_Show_Transactions()
        {
            const decimal amount = 200;
            await _service.AddPayment(_cardId, new AddPaymentModel
            {
                Amount = amount,
                Date = DateTimeOffset.Now
            });
            await Task.Delay(TimeSpan.FromMilliseconds(200));
            var transactions = await _service.GetTransactions(_cardId);

            Assert.True(transactions.Any(x => x.Amount == amount));
        }

        [Test]
        public async Task When_Get_Subscriptions_Return_Three_Subscriptions()
        {
            var result = await _service.GetSubscriptions();
            var ids = result.Select(x => x.Id);
            Assert.That(ids, Contains.Item(DefaultCreditCardSubSubscriptions.BronzeId));
            Assert.That(ids, Contains.Item(DefaultCreditCardSubSubscriptions.GoldId));
            Assert.That(ids, Contains.Item(DefaultCreditCardSubSubscriptions.SilverId));
        }

        [Test]
        public async Task When_AddPayment_To_Non_Existing_card_return_NotFound()
        {
            var result = await _service.AddPayment(CreditCardId.NewId(), new AddPaymentModel
            {
                Date = DateTimeOffset.Now,
                Amount = 200
            });

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}