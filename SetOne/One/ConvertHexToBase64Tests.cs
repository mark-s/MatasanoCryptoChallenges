using FluentAssertions;
using NUnit.Framework;

namespace SetOne.One
{

    [TestFixture]
    public class ConvertHexToBase64Tests
    {

        private IConvertHexToBase64 _converter;

        [SetUp]
        public virtual void SetUp()
        {
            _converter = new ConvertHexToBase64();
        }


        [Test]
        public void Convert_String_ReturnsBase64()
        {
            // Arrange
            const string TEST_HEX_STRING = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
            const string EXPECTED_BASE64_STRING = "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t";
            // Act
            var result = _converter.ConvertToBase64(_converter.GetBytesFromhexString(TEST_HEX_STRING));
            // Assert
            result.Should().Be(EXPECTED_BASE64_STRING);

        }
    }

}
