using FluentAssertions;
using NUnit.Framework;
using SetOne.One;
using SetOne.Two;

namespace SetOne.Three
{
    [TestFixture]
    public class SingleByteXORCipherTests
    {

        private SingleByteXORCipher _xorCipher;

        [SetUp]
        public virtual void SetUp()
        {
            _xorCipher = new SingleByteXORCipher(new FixedXOR());
        }

        [Test]
        public void Unencrypt_GivenhexEncodedString_ReturnUnEncryptedPlaintext()
        {
            // Arrange
            var secretHex = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";
            var expected = "Cooking MC's like a pound of bacon";
            var convertor = new ConvertHexToBase64();
            var secretHexBytes = convertor.GetBytesFromhexString(secretHex);

            // Act
            string res = _xorCipher.GetUnencryptedText(secretHexBytes);

            // Assert
            res.Should().Be(expected);
        }

    }
}
