using System.Collections.Generic;
using MediatR;
using PaymentSystem.Contracts.Models;

namespace PaymentSystem.Application.Queries
{
    public class GetAllSubscriptions:IRequest<IEnumerable<Subscription>>
    {
        
    }
    
}