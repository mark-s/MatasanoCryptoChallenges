using FluentAssertions;
using NUnit.Framework;
using SetTwo.One;

namespace SetTwo.Tests.One
{
    [TestFixture]
    public class PKCS7PaddingTests
    {
        private PKCS7Padding _padder;

        [SetUp]
        public void Setup()
        {
            _padder = new PKCS7Padding();
        }

        [Test]
        public void GetPaddedText_GivenText_PadsCorrectly()
        {

            // Arrange
            const string TEST_STRING = "YELLOW SUBMARINE";
            const string EXPECTED_STRING = @"YELLOW SUBMARINE\x04\x04\x04\x04";

            // Act
            string result = _padder.GetPaddedText(TEST_STRING, 20);


            // Assert
            result.Should().Be(EXPECTED_STRING);

        }


        [Test]
        public void GetByteCount_GivernYELLOWSUBMARINE_Return16()
        {

            // From one of the previous challenges: "YELLOW SUBMARINE" [is] exactly 16 bytes long
            
            // Arrange
            const string TEST_STRING = "YELLOW SUBMARINE";

            // Act
            var result = _padder.GetByteCount(TEST_STRING);

            // Assert
            result.Should().Be(16);

        }


}
}
