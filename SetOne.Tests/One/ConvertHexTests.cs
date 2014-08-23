using FluentAssertions;
using MatasantoCrypto.Set1.One;
using NUnit.Framework;

namespace SetOne.Tests.One
{
    [TestFixture]
    public class ConvertHexTests
    {

        IConvertHex _converter;

        [SetUp]
        public virtual void SetUp()
        {
            _converter = new ConvertHex();
        }

        [Test]
        public void HexStringToByteArray_HexString_ConvertsOK()
        {
            // Arrange
            const string TEST_HEX_STRING = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
            var expected = new byte[] { 73, 39, 109, 32, 107, 105, 108, 108, 105, 110, 103, 32, 121, 111, 117, 114,
                                                      32, 98, 114, 97, 105, 110, 32, 108, 105, 107, 101, 32, 97, 32, 112, 111, 105,
                                                      115, 111, 110, 111, 117, 115, 32, 109, 117, 115, 104, 114, 111, 111, 109 };

            // Act
            var result = _converter.HexStringToByteArray(TEST_HEX_STRING);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ByteArrayToBase64String_String_ReturnsBase64()
        {
            // Arrange
            const string TEST_HEX_STRING = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
            const string EXPECTED_BASE64_STRING = "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t";

            // Act
            var result = _converter.ByteArrayToBase64String(_converter.HexStringToByteArray(TEST_HEX_STRING));

            // Assert
            result.Should().Be(EXPECTED_BASE64_STRING);
        }


    }
}
