using System;
using System.Collections.Generic;
using PaymentSystem.Contracts.Models;

namespace PaymentSystem.ReadModel.Projections
{
    public class CreditCardSubscriptionModelProjection : IProjection
    {
        public static readonly Guid Id = Guid.Parse("CFA6C5ED-FC35-4B59-9602-56DD557E8EC7");
        public IList<Subscription> Subscriptions = new List<Subscription>();
        public Guid ProjectionId { get; set; } = Id;
    }
}