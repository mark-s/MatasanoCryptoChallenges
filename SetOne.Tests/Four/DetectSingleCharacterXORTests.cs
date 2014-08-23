using FluentAssertions;
using MatasantoCrypto.Set1.Four;
using MatasantoCrypto.Set1.One;
using MatasantoCrypto.Set1.Three;
using MatasantoCrypto.Set1.Two;
using NUnit.Framework;

namespace SetOne.Tests.Four
{
    [TestFixture]
    public class DetectSingleCharacterXORTests
    {
        private IDetectSingleCharacterXOR _detector;

        [SetUp]
        public virtual void SetUp()
        {
            _detector = new DetectSingleCharacterXOR(new SingleByteXORCipher(new FixedXOR()), new ConvertHex());
        }

        [Test]
        public void Unencrypt_GivenHexEncodedString_ReturnUnEncryptedPlaintext()
        {
            // Arrange
            const string FILE_NAME = @".\Four\4.txt";
            const string EXPECTED = "Now that the party is jumping\n";

            // Act
            var result = _detector.DetectXOR(FILE_NAME);

            // Assert
            result.Text.Should().Be(EXPECTED);
        }
    }
}
