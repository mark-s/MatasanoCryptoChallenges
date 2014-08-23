using System.IO;
using System.Linq;
using MatasantoCrypto.Set1.One;
using MatasantoCrypto.Set1.SharedCore;
using MatasantoCrypto.Set1.Three;

namespace MatasantoCrypto.Set1.Four
{
    public interface IDetectSingleCharacterXOR
    {
        ResultItem DetectXOR(string filename);
    }

    public class DetectSingleCharacterXOR : IDetectSingleCharacterXOR
    {
        private readonly ISingleByteXORCipher _xorCipher;
        private readonly ConvertHex _convertHex;

        public DetectSingleCharacterXOR(ISingleByteXORCipher xorCipher, ConvertHex convertHex)
        {
            _xorCipher = xorCipher;
            _convertHex = convertHex;
        }

        public ResultItem DetectXOR(string filename)
        {
            return CrackXOR(File.ReadAllLines(filename));
        }

        public ResultItem CrackXOR(string[] xoredHexString)
        {

            return xoredHexString.Select(line => _xorCipher.GetUnencryptedText(_convertHex.HexStringToByteArray(line)))
                                                  .OrderByDescending(i => i.Score)
                                               .First();
        }


    }
}
