using System.Threading.Tasks;
using MediatR;
using PaymentSystem.Domain;

namespace PaymentSystem.Infrastructure.Services
{
    public class MediatorEventPublisher : IEventPublisher
    {
        private readonly IMediator _mediator;

        public MediatorEventPublisher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task PublishAsync(Event evt)
        {
            return _mediator.Publish(evt);
        }
    }
}