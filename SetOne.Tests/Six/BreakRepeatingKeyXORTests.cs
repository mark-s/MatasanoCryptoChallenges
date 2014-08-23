using MatasantoCrypto.Set1.Six;
using MatasantoCrypto.Set1.Three;
using MatasantoCrypto.Set1.Two;
using NUnit.Framework;

namespace SetOne.Tests.Six
{
    [TestFixture]
    public class BreakRepeatingKeyXORTests
    {
        private BreakRepeatingKeyXOR _breaker;

        [SetUp]
        public virtual void SetUp()
        {
            _breaker = new BreakRepeatingKeyXOR(new HammingDistanceCalculator(), new SingleByteXORCipher(new FixedXOR()),new FixedXOR());
        }


        [Test]
        public void Decrypt_GivenTestFileName_DecryptsCorrectly()
        {
            // Arrange
            _breaker.Decrypt(@".\Six\6.txt", 2,40);

            // Act

            // Assert
            
        }



    }
}
