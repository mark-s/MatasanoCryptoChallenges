using FluentAssertions;
using MatasantoCrypto.Set1.Six;
using NUnit.Framework;

namespace SetOne.Tests.Six
{
    [TestFixture]
    public class HammingDistanceCalculatorTests
    {
        private IGetHammingDistance _calc;

        [SetUp]
        public virtual void SetUp()
        {
            _calc = new HammingDistanceCalculator();
        }

        [Test]
        public void GetHammingDistance_GivenTestStrings_ReturnsExpected()
        {
            // Arrange
            const string STRING1 = "this is a test";
            const string STRING2 = "wokka wokka!!!";

            // Act
            var result = _calc.GetHammingDistance(STRING1, STRING2);

            // Assert
            result.Should().Be(37);
        }


    }
}
