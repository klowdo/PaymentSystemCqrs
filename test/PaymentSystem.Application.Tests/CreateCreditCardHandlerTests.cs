using System;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using PaymentSystem.Application.Handlers;
using PaymentSystem.Domain;
using PaymentSystem.Domain.Models.CreditCards;
using PaymentSystem.Domain.Models.CreditCards.Commands;
using PaymentSystem.Domain.Models.CreditCardSubscriptions;

namespace PaymentSystem.Application.Tests
{
    public class CreateCreditCardHandlerTests
    {
        [Test]
        public void When_CreateCardCommand_Subscription_Does_Not_exist_Throw_InvalidOperationException()
        {
            var repo = A.Fake<IAggregateRepository<CreditCard>>();
            var subRepository = A.Fake<IAggregateRepository<CreditCardSubscription>>();
            A.CallTo(() => subRepository.ExistsAsync(A<CreditCardSubscriptionId>._))
                .Returns(false);
            var clock = A.Fake<ISystemClock>();
            var sut = new CreateCreditCardHandler(repo, subRepository, clock);

            Assert.ThrowsAsync<InvalidOperationException>(
                () => sut.Handle(
                    new CreateCreditCard(CreditCardId.NewId(), CreditCardSubscriptionId.NewId(), DateTimeOffset.Now),
                    CancellationToken.None)
            );
        }

        [Test]
        public async Task When_CreateCardCommand_Valid_Arguments_Saves_Card()
        {
            var repo = A.Fake<IAggregateRepository<CreditCard>>();
            var subRepository = A.Fake<IAggregateRepository<CreditCardSubscription>>();
            A.CallTo(() => subRepository.ExistsAsync(A<CreditCardSubscriptionId>._))
                .Returns(true);
            var clock = A.Fake<ISystemClock>();
            var sut = new CreateCreditCardHandler(repo, subRepository, clock);

            await sut.Handle(
                new CreateCreditCard(CreditCardId.NewId(), CreditCardSubscriptionId.NewId(), DateTimeOffset.Now),
                CancellationToken.None);

            A.CallTo(() => repo.SaveAsync(A<CreditCard>._, A<long>._))
                .MustHaveHappenedOnceExactly();
        }
    }
}