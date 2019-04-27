using System;
using System.Collections.Generic;
using PaymentSystem.Contracts.Models;

namespace PaymentSystem.ReadModel
{
    public class AllCreditCardsProjections : IProjection
    {
        public static Guid Id = Guid.Parse("5233A951-D552-48A0-864D-44D8FDE49E86");
        public Guid ProjectionId { get; set; } = Id;
        public IList<CreditCardModel> Cards { get; set; } = new List<CreditCardModel>();
    }
}