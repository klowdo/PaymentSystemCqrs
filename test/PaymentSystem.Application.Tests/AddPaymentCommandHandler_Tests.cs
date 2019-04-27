using System;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using PaymentSystem.Application.Handlers;
using PaymentSystem.Contracts.Models;
using PaymentSystem.Domain;
using PaymentSystem.Domain.Models.CreditCards;
using PaymentSystem.Domain.Models.CreditCards.Commands;
using PaymentSystem.Domain.Models.CreditCardSubscriptions;

namespace PaymentSystem.Application.Tests
{
    public class AddPaymentCommandHandler_Tests
    {
        [Test]
        public async Task When_AddPaymentCommand_Get_Subscription_Corresponding_To_CreditCard()
        {
            var subId = CreditCardSubscriptionId.NewId();
            var creditCard = new CreditCard(CreditCardId.NewId(), subId, DateTimeOffset.Now);

            var repo = A.Fake<IAggregateRepository<CreditCard>>();
            A.CallTo(() => repo.GetByIdAsync(creditCard.Id))
                .Returns(creditCard);
            var subRepository = A.Fake<IAggregateRepository<CreditCardSubscription>>();
            var sub = new CreditCardSubscription(subId,DateTimeOffset.Now, new CreditCardSubscriptionName("gold"));
            sub.UseNoPaymentFee(DateTimeOffset.Now);
            A.CallTo(subRepository)
                .WithReturnType<Task<CreditCardSubscription>>()
                .Returns(sub);

            var sut = new AddPaymentCommandHandler(repo, subRepository);
            await sut.Handle(new AddPaymentCommand
            {
                AggregateId = creditCard.Id,
                Payment = new Payment(PaymentId.NewId(), Money.CreateAUD(200), DateTimeOffset.Now)
            }, CancellationToken.None);

            A.CallTo(() => subRepository.GetByIdAsync(subId))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => repo.SaveAsync(A<CreditCard>._, A<long>._))
                .MustHaveHappenedOnceExactly();
        }
    }
}