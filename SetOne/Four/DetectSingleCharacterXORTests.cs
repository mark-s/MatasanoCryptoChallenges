using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SetOne.One;
using SetOne.Three;
using SetOne.Two;

namespace SetOne.Four
{
    [TestFixture]
    public class DetectSingleCharacterXORTests
    {

        private IDetectSingleCharacterXOR _detector;

        [SetUp]
        public virtual void SetUp()
        {
            _detector = new DetectSingleCharacterXOR(new SingleByteXORCipher(new FixedXOR()), new ConvertHexToBase64());
        }


        [Test]
        public void Unencrypt_GivenHexEncodedString_ReturnUnEncryptedPlaintext()
        {

            // Arrange
            const string FILE_NAME = (@"C:\Dropbox\FastDev\MatasanoCryptoChallenges\SetOne\Four\4.txt");
            const string EXPECTED = "Now that the party is jumping\n";

            //Act
            string result = _detector.DetectXOR(FILE_NAME);

            // Assert
            result.Should().Be(EXPECTED);
            
        }


    }
}
