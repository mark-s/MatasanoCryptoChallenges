using FluentAssertions;
using MatasantoCrypto.Set1.Two;
using NUnit.Framework;

namespace MatasantoCrypto.Set1.Five
{
    [TestFixture]
    public class RepeatingKeyXORTests
    {
        private RepeatingKeyXOR _encryptor;

        [SetUp]
        public virtual void SetUp()
        {
            _encryptor = new RepeatingKeyXOR(new FixedXOR());
        }

        [Test]
        public void Encode_GoodText_EncryptsToXOR()
        {
            // Arrange
            // NOTE" the newline is just /n 
            string PLAINTEXT = "Burning 'em, if you ain't quick and nimble\nI go crazy when I hear a cymbal";

            string EXPECTED = "0b3637272a2b2e63622c2e69692a23693a2a3c6324202d623d63343c2a26226324272765272" +
                                         "a282b2f20430a652e2c652a3124333a653e2b2027630c692b20283165286326302e27282f";

            

            const string ENCRYPTION_KEY = "ICE";

            // Act
            string result = _encryptor.EncryptWithRepeatingKey(PLAINTEXT, ENCRYPTION_KEY);
            // Assert
            result.Should().Be(EXPECTED);
        }

    }
}
