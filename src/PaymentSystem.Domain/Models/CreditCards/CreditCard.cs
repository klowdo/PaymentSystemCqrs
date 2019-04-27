using System;
using System.Collections.Generic;

namespace PaymentSystem.Domain
{
    public class CreditCard:Entity<Guid>
    {
        public CreditCard(Guid id) : base(id)
        {
        }
        private readonly IList<Payment> _payments = new List<Payment>();

        public void Add(Payment payment)
        {
            if (payment != null) _payments.Add(payment);
        }
    }
}