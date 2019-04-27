using System;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using PaymentSystem.Domain.Models;
using PaymentSystem.Domain.Models.CreditCards;
using PaymentSystem.Domain.Models.CreditCards.Events;
using PaymentSystem.Domain.Models.CreditCardSubscriptions;
using PaymentSystem.Domain.Models.CreditCardSubscriptions.Events;

namespace PaymentSystem.Domain.Tests
{
    public class CreditCardSubscription_Test
    {
        [Test]
        public void When_Created_subscription_not_Active()
        {
            var sut = CreateSut();

            Assert.That(sut.Active, Is.False);
        }

        [Test]
        public void When_Created_and_assigned_strategy_then_Active()
        {
            var sut = CreateSut();
            sut.UseNoPaymentFee(DateTimeOffset.Now);

            Assert.That(sut.Active, Is.True);
        }


        [Test]
        public void When_AddTransaction_To_card_with_other_subscription_Throw_InvalidOperationException()
        {
            var sut = CreateSut();

            var card = CreateCard();

            Assert.Throws<InvalidOperationException>(() =>
                sut.AddTransactionTo(card, A.Dummy<Payment>(), A.Dummy<DateTimeOffset>()));
        }

        [Test]
        public void When_UseFixedPaymentFee_Applies_UseFixedPaymentFee()
        {
            var sut = CreateSut();

            sut.UseFixedPaymentFee(DateTimeOffset.Now, Money.CreateAUD(200));

            var evt = sut.GetUncommittedChanges().Last();

            if (evt is UseFixedPaymentFee usefee)
                Assert.That(usefee.Fee, Is.EqualTo(Money.CreateAUD(200)));
            else
                Assert.Fail("No Event Created");
        }

        [Test]
        public void When_UseRatePaymentFee_Applies_UseRatePaymentFee()
        {
            var sut = CreateSut();

            sut.UseRatePaymentFee(DateTimeOffset.Now, Rate.Create(0.5m));

            var evt = sut.GetUncommittedChanges().Last();

            if (evt is UseRatePaymentFee usefee)
                Assert.That(usefee.FeeRate, Is.EqualTo(Rate.Create(0.5m)));
            else
                Assert.Fail("No Event Created");
        }

        [Test]
        public void When_UseNoPaymentFee_Applies_UseNoPaymentFee()
        {
            var sut = CreateSut();

            sut.UseNoPaymentFee(DateTimeOffset.Now);

            var evt = sut.GetUncommittedChanges().Last();

            if (!(evt is UseNoPaymentFee))
                Assert.Fail("No Event Created");
        }

        [Test]
        public void When_Fee_Apply_Add_Additional_Transaction()
        {
            var fee = Money.CreateAUD(1337);
            var sut = CreateSut();
            var card = CreateCard(subscriptionId: sut.Id);
            sut.UseFixedPaymentFee(DateTimeOffset.Now, fee);

            sut.AddTransactionTo(card, new Payment(PaymentId.NewId(), Money.CreateAUD(200), DateTimeOffset.Now),
                DateTimeOffset.Now);

            Assert.True(card.GetUncommittedChanges().OfType<CreditCardTransactionAdded>()
                .Any(x => x.Transaction.Value == fee
                          && x.Transaction.Type == TransactionType.Fee));
        }

        [Test]
        public void When_No_Fee_Apply_do_not_add_Additional_Transaction()
        {
            var fee = Money.CreateAUD(1337);
            var sut = CreateSut();
            var card = CreateCard(subscriptionId: sut.Id);
            sut.UseNoPaymentFee(DateTimeOffset.Now);

            sut.AddTransactionTo(card, new Payment(PaymentId.NewId(), Money.CreateAUD(200), DateTimeOffset.Now),
                DateTimeOffset.Now);

            Assert.False(card.GetUncommittedChanges().OfType<CreditCardTransactionAdded>()
                .Any(x => x.Transaction.Type == TransactionType.Fee));
        }


        public static CreditCard CreateCard(CreditCardId? id = null, CreditCardSubscriptionId? subscriptionId = null)
        {
            return new CreditCard(
                id ?? CreditCardId.NewId(),
                subscriptionId ?? CreditCardSubscriptionId.NewId(),
                DateTimeOffset.Now
            );
        }

        private static CreditCardSubscription CreateSut()
        {
            var id = CreditCardSubscriptionId.NewId();
            var name = new CreditCardSubscriptionName("Silver");
            return new CreditCardSubscription(id, DateTimeOffset.Now, name);
        }
    }
}