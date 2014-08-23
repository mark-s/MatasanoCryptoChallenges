using FluentAssertions;
using MatasantoCrypto.Set1.One;
using MatasantoCrypto.Set1.SharedCore;
using MatasantoCrypto.Set1.Three;
using MatasantoCrypto.Set1.Two;
using NUnit.Framework;

namespace SetOne.Tests.Three
{
    public class SingleByteXORCipherTests
    {
        private SingleByteXORCipher _xorCipher;

        [SetUp]
        public virtual void SetUp()
        {
            _xorCipher = new SingleByteXORCipher(new FixedXOR());
        }

        [Test]
        public void Unencrypt_GivenHexEncodedString_ReturnUnEncryptedPlaintext()
        {
            // Arrange
            var secretHex = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";
            var expected = "Cooking MC's like a pound of bacon";

            var convertor = new ConvertHex();
            var secretHexBytes = convertor.HexStringToByteArray(secretHex);
            // Act

            ResultItem res = _xorCipher.GetUnencryptedText(secretHexBytes);

            // Assert
            res.Text.Should().Be(expected);
        }


    }
}
