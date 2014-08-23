using FluentAssertions;
using MatasantoCrypto.Set1.One;
using MatasantoCrypto.Set1.Two;
using NUnit.Framework;

namespace SetOne.Tests.Two
{
    [TestFixture]
    public class FixedXORTests
    {
        private IFixedXOR _fixedXOR;

        [SetUp]
        public virtual void SetUp()
        {
            _fixedXOR = new FixedXOR();
        }


        [Test]
        public void GetFixedXOR_GivenHexStrings_GivesGoodXOR()
        {
            // Arrange
            const string STRING1 = "1c0111001f010100061a024b53535009181c";
            const string STRING2 = "686974207468652062756c6c277320657965";
            const string EXPECTED = "746865206b696420646f6e277420706c6179";

            var convertor = new ConvertHex();

            byte[] str1AsBytes = convertor.HexStringToByteArray(STRING1);
            byte[] str2AsBytes = convertor.HexStringToByteArray(STRING2);

            // Act
            var result = _fixedXOR.XOR(str1AsBytes, str2AsBytes);

            // Assert
            result.Should().Be(EXPECTED);
        }


    }
}
