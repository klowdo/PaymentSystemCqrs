using System;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using PaymentSystem.Contracts;
using PaymentSystem.Contracts.Models;
using Refit;

namespace PaymentSystem.Portal.Tests
{
    public class CreateCreditCardErrorTest : PaymentSystemWebApplicationFactory<Startup>
    {
        private IPaymentsService _service;

        [SetUp]
        public void SetUp()
        {
            _service = RestService.For<IPaymentsService>(CreateClient());
        }

        [Test]
        public async Task When_Wrong_SubscriptionId_Inserted_Return_BadRequest()
        {
            var result = Assert.ThrowsAsync<ValidationApiException>(async () => await _service.CreateCard(
                new CreateCreditCardModel
                {
                    CreditCardSubscriptionId = Guid.Parse("6E90C9E0-74A8-4EEC-B4E9-5A89BAEA3C13")
                }));

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));


            Assert.That(result.Content.Errors[nameof(CreateCreditCardModel.CreditCardSubscriptionId)],
                Contains.Item("Subscription does not exist"));
        }
    }
}