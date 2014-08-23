using System;
using System.Linq;
using System.Text;
using MatasantoCrypto.Set1.SharedCore;
using MatasantoCrypto.Set1.Two;

namespace MatasantoCrypto.Set1.Five
{
    public class RepeatingKeyXOR
    {
        private readonly IFixedXOR _fixedXOR;
        
        public RepeatingKeyXOR(IFixedXOR fixedXOR)
        {
            _fixedXOR = fixedXOR;
        }

        public string EncryptWithRepeatingKey(string plainText, string encryptionKey)
        {
            var sb = new StringBuilder();

            var lines = plainText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var totalCharCount = lines.Sum(line => line.Length);

            var enumerableRepeatingKey = Encoding.UTF8.GetBytes(encryptionKey.Repeat(totalCharCount));

            foreach (var line in lines)
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(line);
                var encoded = EncodeForOneLine(plainTextBytes, enumerableRepeatingKey);
                sb.Append(encoded);
            }

            return sb.ToString();
        }

        public string EncodeForOneLine(byte[] lineBytes, byte[] encryptionKeyBytes)
        {
            string toReturn = "";

            for (int j = 0; j < lineBytes.Length; j++)
            {
                toReturn += _fixedXOR.XOR(lineBytes[j], encryptionKeyBytes[j]);
            }

            return toReturn;
        }





    }
}
