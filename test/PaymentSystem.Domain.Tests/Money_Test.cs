using NUnit.Framework;

namespace PaymentSystem.Domain.Tests
{
    public class Money_Test
    {
        [Test]
        public void When_Addition_onePlusOneSek_Returns2()
        {
            var actual = Money.CreateAUD(1) + Money.CreateAUD(1);
            Assert.AreEqual(2m, actual.Value);
        }

        [Test]
        public void When_Subtraction_oneMinusOneSek_Returns2()
        {
            var actual = Money.CreateAUD(1) - Money.CreateAUD(1);
            Assert.AreEqual(decimal.Zero, actual.Value);
        }

        [Test]
        public void When_Multiply_oneMultiplyWith2_Returns2()
        {
            var actual = Money.CreateAUD(1) * 2;
            Assert.AreEqual(2m, actual.Value);
        }

        [Test]
        public void When_Divide_TwoDeiceWith2_Returns1()
        {
            var actual = Money.CreateAUD(2) / 2;
            Assert.AreEqual(1, actual.Value);
        }

        [Test]
        public void When_MoneyZero_Returns0()
        {
            var actual = Money.Zero(CurrencyCode.SEK);
            Assert.AreEqual(decimal.Zero, actual.Value);
        }

        [Test]
        public void When_MoneyEquals_MoneyValueAndCurrencyEqual_ReturnsTrue()
        {
            var money1 = Money.CreateAUD(2);
            var money2 = Money.CreateAUD(2);
            Assert.AreEqual(money1, money2);
        }

        [Test]
        public void When_MoneyEquals_MoneyValueAndCurrencyNotEqual_ReturnsFalse()
        {
            Assert.AreNotEqual(Money.CreateAUD(2), Money.CreateAUD(4));
            Assert.AreNotEqual(Money.Create(1, CurrencyCode.AUD), Money.Create(1, CurrencyCode.SEK));
        }

        [Test]
        public void When_Money_is_with_decimal_above_or_equal_4_50_round_up()
        {
            var decimalMoney = Money.CreateAUD(4.50m);
            var roundedMoney = decimalMoney.RoundOff();
            Assert.That(roundedMoney, Is.EqualTo(Money.CreateAUD(5)));
        }

        [Test]
        public void When_Money_is_with_decimal_above_or_equal_4_49_round_down()
        {
            var decimalMoney = Money.CreateAUD(4.49m);
            var roundedMoney = decimalMoney.RoundOff();
            Assert.That(roundedMoney, Is.EqualTo(Money.CreateAUD(4)));
        }
    }
}