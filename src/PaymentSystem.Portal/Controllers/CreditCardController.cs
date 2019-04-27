using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PaymentSystem.Application;
using PaymentSystem.Application.Queries;
using PaymentSystem.Contracts.Models;
using PaymentSystem.Domain;
using PaymentSystem.Domain.Models.CreditCards.Commands;
using PaymentSystem.Infrastructure;
using PaymentSystem.Portal.Models.Mappers;
using PaymentSystem.Portal.Options;

namespace PaymentSystem.Portal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditCardController : Controller
    {
        private readonly ISystemClock _clock;

        //Move to config then userContext or from request;
        private readonly CurrencyCode _currencyCode;
        private readonly IMediator _mediator;

        public CreditCardController(IMediator mediator, ISystemClock clock, IOptions<PaymentSystemOptions> options)
        {
            _mediator = mediator;
            _clock = clock;
            _currencyCode = options.Value.DefaultCurrencyCode;
        }

        [HttpPost("create-card")]
        public async Task<ActionResult<CreditCardCreatedResponse>> AddCard([FromBody] CreateCreditCardModel model)
        {
            var cardId = CreditCardId.NewId();
            await _mediator.Send(new CreateCreditCard(cardId, model.CreditCardSubscriptionId, _clock.Now));
            return Ok(new CreditCardCreatedResponse {CreditCardId = cardId});
        }

        [HttpPost("{cardId}/add-payment")]
        public async Task<IActionResult> AddPayment([FromRoute] CreditCardId cardId, [FromBody] AddPaymentModel model)
        {
            try
            {
                await _mediator.Send(model.ToPaymentCommand(cardId, _clock, _currencyCode));
                return Ok();
            }
            catch (AggregateNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CreditCardModel>>> GetAllCards()
        {
            return Ok(await _mediator.Send(new GetAllCards()));
        }

        [HttpGet("subscriptions")]
        public async Task<ActionResult<IEnumerable<Subscription>>> Subscriptions()
        {
            return Ok(await _mediator.Send(new GetAllSubscriptions()));
        }

        [HttpGet("{cardId}/transactions")]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> ListPayments([FromRoute] CreditCardId cardId)
        {
            return Ok(await _mediator.Send(new GetAllPayments(cardId)));
        }
    }
}