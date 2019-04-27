using System;
using System.Linq;
using NUnit.Framework;
using PaymentSystem.Domain.Models.CreditCards;
using PaymentSystem.Domain.Models.CreditCards.Events;

namespace PaymentSystem.Domain.Tests
{
    public class CreditCard_Test
    {
        [Test]
        public void When_CreditCard_Created_Applies_CreditCardCreated()
        {
            var id = CreditCardId.NewId();
            var subId = CreditCardSubscriptionId.NewId();
            var sut = new CreditCard(id, subId, DateTimeOffset.Now);

            var actual = sut.GetUncommittedChanges();

            Assert.True(actual.OfType<CreditCardCreated>()
                .Any(x => x.CreditCardSubscription == subId && x.AggregateId == id));

            Assert.That(sut.SubscriptionId, Is.EqualTo(subId));
        }

        [Test]
        public void When_CreditCard_AddTransaction_Applies_CreditCardTransactionAdded()
        {
            var id = CreditCardId.NewId();
            var subId = CreditCardSubscriptionId.NewId();

            var transaction =
                Transaction.CreatePayment(new Payment(PaymentId.NewId(), Money.CreateAUD(200), DateTimeOffset.Now));

            var sut = new CreditCard(id, subId, DateTimeOffset.Now);

            var actual = sut.GetUncommittedChanges();
            sut.AddTransaction(transaction, DateTimeOffset.Now);

            Assert.True(actual.OfType<CreditCardTransactionAdded>().Any(x =>
                x.Transaction.Id == transaction.Id
                && x.Transaction.Value == transaction.Value
                && x.AggregateId == id));
        }
    }
}