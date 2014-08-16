using System;
using System.Diagnostics;
using System.Text;
using SetOne.Two;

namespace SetOne.Five
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
            var enumerableRepeatingKey = encryptionKey.Repeat(plainText.Length);

            foreach (var line in lines)
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(line.Trim());
                var encoded = EncodeForOneLine(plainTextBytes, enumerableRepeatingKey);
                sb.AppendLine(encoded);
            }

            return sb.ToString();

        }



        private string EncodeForOneLine(byte[] line, char[] encryptionKey)
        {
            string toReturn = "";

            Debug.WriteLine(Encoding.UTF8.GetString(line));

            for (int j = 0; j < line.Length; j++)
            {
                toReturn += _fixedXOR.GetFixedXOR(line[j], Encoding.UTF8.GetBytes(encryptionKey[j].ToString())[0]);
            }

            return toReturn;
        }




    }
}
