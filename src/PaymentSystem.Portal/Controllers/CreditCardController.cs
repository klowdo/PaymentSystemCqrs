using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Application;
using PaymentSystem.Application.Handlers;
using PaymentSystem.Contracts.Models;
using PaymentSystem.Domain;
using PaymentSystem.Domain.Models.CreditCards.Commands;
using PaymentSystem.Portal.Models;
using PaymentSystem.Portal.Models.Mappers;

namespace PaymentSystem.Portal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditCardController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ISystemClock _clock;

        //Move to condig then userContext or from request;
        private readonly CurrencyCode _currencyCode = CurrencyCode.AUD;

        public CreditCardController(IMediator mediator, ISystemClock clock)
        {
            _mediator = mediator;
            _clock = clock;
        }

        [Route("create-card")]
        public async Task<IActionResult> AddCard([FromRoute] CreditCardId cardId)
        {
            await _mediator.Send(new CreateCreditCard(cardId, _clock.Now));
            return Ok();
        }
        [Route("{cardId}/add-payment")]
        public async Task<IActionResult> AddPayment([FromRoute] CreditCardId cardId, [FromBody] AddPaymentModel model)
        {
            await _mediator.Send(model.ToPaymentCommand(cardId,_clock, _currencyCode));
            return Ok();
        }

//        [Route("{cardId}/transactions")]
//        public async Task<IActionResult> ListPayments([FromRoute] CreditCardId cardId, AddPaymentModel model)
//        {
//            var result = _mediator.Send(new ListPaymentsRequest())
//            await _mediator.Publish(model.ToPaymentCommand(PaymentType.Gold, _currencyCode));
//            return Ok();
//        }
    }
}