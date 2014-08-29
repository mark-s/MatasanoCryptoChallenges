using System;
using System.Linq;
using System.Text;

namespace SetTwo.One
{
    public class PKCS7Padding
    {

        // See: http://www.wikiwand.com/en/Padding_(cryptography)
        // and : http://tools.ietf.org/html/rfc2315
        // For such algorithms, the method shall be to pad the input at the trailing end with:
        //  k - (l mod k) octets all having value k - (l mod k), where l is the length of the input.


        public string GetPaddedText(string textToPad, int blockLength)
        {
            var inputTextLengthInBytes = GetByteCount(textToPad);

            if (inputTextLengthInBytes > 256 || blockLength > 256)
                throw new ArgumentOutOfRangeException("blockLength", blockLength, "This padding method is well-defined if and only if k < 256");

            var paddingBytesNeeded = (blockLength % inputTextLengthInBytes);

            var paddingCountAsHexString = String.Format("\\x{0:x2}", paddingBytesNeeded);

            var toAppend = String.Concat(Enumerable.Repeat(paddingCountAsHexString, paddingBytesNeeded));

            return textToPad + toAppend;
        }


        public int GetByteCount(string stringTocount)
        {
            return Encoding.UTF8.GetByteCount(stringTocount);
        }


    }
}
