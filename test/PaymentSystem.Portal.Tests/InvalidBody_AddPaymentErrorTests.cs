using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using PaymentSystem.Contracts;
using PaymentSystem.Contracts.Models;
using PaymentSystem.Portal.TempData;
using Refit;

namespace PaymentSystem.Portal.Tests
{
    [TestFixture]
    public class InvalidBody_AddPaymentErrorTests : PaymentSystemWebApplicationFactory<Startup>
    {
        [SetUp]
        public async Task SetUp()
        {
            _service = RestService.For<IPaymentsService>(CreateClient());
            var response = await _service.CreateCard(new CreateCreditCardModel
            {
                CreditCardSubscriptionId = DefaultCreditCardSubSubscriptions.BronzeId
            });
            _cardId = response.CreditCardId;
            _result = await _service.AddPayment(_cardId, new AddPaymentModel
            {
                Date = DateTimeOffset.Now.AddDays(5)
            });
            _problemDetails = await _result.ProblemDetails();
        }

        private Guid _cardId;
        private IPaymentsService _service;
        private HttpResponseMessage _result;
        private ProblemDetails _problemDetails;

        [Test]
        public void When_amount_Is_Invalid_Return_ErrorMessage()
        {
            Assert.That(_problemDetails.Errors, Contains.Key(nameof(AddPaymentModel.Amount)));
        }

        [Test]
        public void When_Date_Is_Invalid_Return_ErrorMessage()
        {
            Assert.That(_problemDetails.Errors, Contains.Key(nameof(AddPaymentModel.Date)));
        }

        [Test]
        public void  When_No_Body_Input_Return_BadRequest()
        {
            Assert.That(_result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }
    }
}