using FluentAssertions;
using MatasantoCrypto.Set1.Eight;
using NUnit.Framework;

namespace SetOne.Tests.Eight
{
    [TestFixture]
    public class DetectAESInECBModeTests
    {
        private DetectAESInECBMode _detector;

        [SetUp]
        public virtual void SetUp()
        {
            _detector = new DetectAESInECBMode();
        }

        [Test]
        public void Detect_GivenText_DetectsEncryptedText()
        {
            // Arrange

            // Act
            var result = _detector.GetEncryptedStrings(@".\Eight\8.txt");

            // Assert
            result.Should().Be("08649af70dc06f4fd5d2d69c744cd283");
        }


    }
}