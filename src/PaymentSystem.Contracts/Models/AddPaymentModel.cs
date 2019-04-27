using System;
using FluentValidation;
using PaymentSystem.Application;
using PaymentSystem.Domain;

namespace PaymentSystem.Portal.Models
{
    public class AddPaymentModel
    {
        public DateTimeOffset Date { get; set; }
        public decimal Amount { get; set; }
        public CurrencyCode CurrencyCode { get; set; }
        
        public class Validator: AbstractValidator<AddPaymentModel>
        {
            public Validator(ISystemClock systemClock)
            {
                RuleFor(x => x.Date).LessThanOrEqualTo(x => systemClock.Now);
                RuleFor(x => x.Amount).GreaterThan(0);
                RuleFor(x => x.CurrencyCode).IsInEnum();
            }
        }
    }
}