using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaymentSystem.Domain;

namespace PaymentSystem.Application.Handelers
{
    public class AddPaymentCommand : IRequest
    {
        public DateTimeOffset Date;
        public Money Amount;
    }
    public class AddPaymentCommandHandler : AsyncRequestHandler<AddPaymentCommand> {

        protected override Task Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}