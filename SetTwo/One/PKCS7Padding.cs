using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SetTwo.One
{
    public class PKCS7Padding
    {

        // See: http://www.wikiwand.com/en/Padding_(cryptography)
        // and : http://tools.ietf.org/html/rfc2315
        // For such algorithms, the method shall be to pad the input at the trailing end with:
        //  k - (l mod k) octets all having value k - (l mod k), where l is the length of the input.


        public byte[] GetPaddedText(string textToPad, int blockLength)
        {
            var inputTextLengthInBytes = GetByteCount(textToPad);

            if (inputTextLengthInBytes > 256 || blockLength > 256)
                throw new ArgumentOutOfRangeException("blockLength", blockLength,
                    "This padding method is well-defined if and only if k < 256");


            var textToPadAsBytes = Encoding.UTF8.GetBytes(textToPad);
 
            var paddingBytesNeeded = (blockLength % inputTextLengthInBytes);

            var asByte = NumberToBytes(paddingBytesNeeded);

            var toReturn = new List<byte>(textToPadAsBytes);

            for (int i = 0; i < paddingBytesNeeded; i++)
            {
                toReturn.Add(asByte);
            }

            return toReturn.ToArray();
        }


        public int GetByteCount(string stringTocount)
        {
            return Encoding.UTF8.GetByteCount(stringTocount);
        }


        private byte NumberToBytes(int number)
        {
            return Convert.ToByte(number.ToString(CultureInfo.InvariantCulture), 16);
        }


    }
}
