using System;
using System.Collections.Generic;
using System.Transactions;
using PaymentSystem.Contracts.Models;
using PaymentSystem.Domain;

namespace PaymentSystem.ReadModel
{
    public class CreditCardSubscriptionModelProjection : IProjection
    {
        public static readonly Guid Id = Guid.Parse("CFA6C5ED-FC35-4B59-9602-56DD557E8EC7");
        public Guid ProjectionId { get; set; } = Id;
        public IList<Subscription> Subscriptions = new List<Subscription>();
    }

   
}