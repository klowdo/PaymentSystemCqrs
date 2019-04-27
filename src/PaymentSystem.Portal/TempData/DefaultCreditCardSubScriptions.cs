using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PaymentSystem.Application;
using PaymentSystem.Domain;
using PaymentSystem.Domain.Models;
using PaymentSystem.Domain.Models.CreditCardSubscriptions;

namespace PaymentSystem.Portal.TempData
{
    public static class DefaultCreditCardSubSubscriptions
    {
        public static Guid SilverId = Guid.Parse("95C64013-388D-4FCA-8643-9FC32EF66EC5");
        public static Guid BronzeId = Guid.Parse("358293A5-13AD-4C5A-83EC-0EEBFFAB91BD");
        public static Guid GoldId = Guid.Parse("424BF988-A1FD-4F1B-A1D4-D9D038F93786");

        public static IApplicationBuilder UseDefaultCreditCardSubSubscriptions(this IApplicationBuilder app)
        {
            var repo = app.ApplicationServices.GetService<IAggregateRepository<CreditCardSubscription>>();
            Task.WaitAll(
                repo.SaveAsync(CreateBronze(), -1),
                repo.SaveAsync(CreateSilver(), -1),
                repo.SaveAsync(CreateGold(), -1)
            );
            return app;
        }

        private static CreditCardSubscription CreateSilver()
        {
            var silver = new CreditCardSubscription(
                new CreditCardSubscriptionId(SilverId), DateTimeOffset.Now,
                new CreditCardSubscriptionName("Silver"));
            silver.UseRatePaymentFee(DateTimeOffset.Now, Rate.Create(0.02m));
            return silver;
        }

        private static CreditCardSubscription CreateBronze()
        {
            var bronze = new CreditCardSubscription(
                new CreditCardSubscriptionId(BronzeId), DateTimeOffset.Now,
                new CreditCardSubscriptionName("Bronze"));
            bronze.UseFixedPaymentFee(DateTimeOffset.Now, Money.CreateAUD(1.25m));
            return bronze;
        }

        private static CreditCardSubscription CreateGold()
        {
            var gold = new CreditCardSubscription(
                new CreditCardSubscriptionId(GoldId), DateTimeOffset.Now,
                new CreditCardSubscriptionName("Gold"));
            gold.UseNoPaymentFee(DateTimeOffset.Now);
            return gold;
        }
    }
}